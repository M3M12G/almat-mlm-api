using System.Text.Json.Serialization;
using Mlm.Api.Data;
using Mlm.Api.Infrastructure;
using Mlm.Api.Infrastructure.Auth;
using Mlm.Api.Infrastructure.Configuration;
using Mapster;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using Quartz;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .Filter.ByExcluding(logEvent =>
            logEvent.Properties.ContainsKey("Email")
            || logEvent.Properties.ContainsKey("Phone")
            || logEvent.Properties.ContainsKey("Iin")
            || logEvent.Properties.ContainsKey("AccessToken"))
        .WriteTo.Console());

    builder.Services.AddProblemDetails(options =>
    {
        options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions["traceId"] =
                ctx.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity?.Id
                ?? ctx.HttpContext.TraceIdentifier;
        };
    });

    builder.Services.AddControllers()
        .AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

    builder.Services.AddOpenApi();
    builder.Services.AddMapster();
    builder.Services.AddMlmInfrastructure(builder.Environment);

    var app = builder.Build();

    app.UseExceptionHandler();
    app.UseStatusCodePages();

    if (!app.Environment.IsDevelopment())
    {
        app.UseForwardedHeaders();
        app.UseHsts();
    }

    app.UseSerilogRequestLogging(opts =>
    {
        opts.GetLevel = (_, _, _) => LogEventLevel.Information;
        opts.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        };
    });

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseCors(WebCorsOptions.PolicyName);
    app.UseMiddleware<DeviceIdCookieMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseAntiforgery();

    var openApi = app.Services.GetRequiredService<IOptions<OpenApiOptions>>().Value;
    if (openApi.Enabled)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options => options.WithTitle(openApi.Title));
    }

    app.MapControllers();
    app.MapQuartzDashboard();
    app.MapHealthChecks("/health", new HealthCheckOptions())
        .AllowAnonymous();

    await app.InitializeDatabaseAsync();
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program;
