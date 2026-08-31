using System.Text.Json.Serialization;
using Mlm.Api.Data;
using Mlm.Api.Infrastructure.Auth;
using Mlm.Api.Infrastructure.Jobs;
using CrystalQuartz.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Quartz;
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
            // Keep Information+ free of typical PII property names.
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

    var connectionString = builder.Configuration.GetConnectionString("Default")
        ?? throw new InvalidOperationException("Connection string 'Default' is missing.");

    if (string.IsNullOrWhiteSpace(builder.Configuration["Quartz:Dashboard:Password"]))
    {
        throw new InvalidOperationException(
            "Quartz:Dashboard:Password must be set (dashboard must not be public).");
    }

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

    builder.Services.AddJwtCookieAuth(builder.Configuration);
    builder.Services.AddAlmatQuartz(connectionString);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Web", policy =>
        {
            var origins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>()
                ?? ["http://localhost:3000"];
            policy.WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

    var app = builder.Build();

    app.UseExceptionHandler();
    app.UseStatusCodePages();

    app.UseSerilogRequestLogging(opts =>
    {
        opts.GetLevel = (_, _, _) => LogEventLevel.Information;
        opts.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            // Intentionally omit query/body/user email — no PII at Information+.
        };
    });

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();
    app.UseCors("Web");
    app.UseAuthentication();
    app.UseAuthorization();

    // AGENTS.md / ADR-0004: CrystalQuartz dashboard must never be public.
    app.UseMiddleware<QuartzDashboardBasicAuthMiddleware>();
    app.UseCrystalQuartz(() =>
        app.Services.GetRequiredService<ISchedulerFactory>().GetScheduler());

    app.MapControllers();
    app.MapGet("/health", () => Results.Ok(new { status = "ok" }))
        .AllowAnonymous();

    app.Run();
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
