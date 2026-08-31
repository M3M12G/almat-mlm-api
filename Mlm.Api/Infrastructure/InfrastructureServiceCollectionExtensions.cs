using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mlm.Api.Data;
using Mlm.Api.Infrastructure.Auth;
using Mlm.Api.Infrastructure.Configuration;
using Mlm.Api.Infrastructure.Encryption;
using Mlm.Api.Infrastructure.Errors;
using Mlm.Api.Infrastructure.Jobs;
using Mlm.Api.Modules.Audit;
using Mlm.Api.Modules.Identity.Services;
using MsCors = Microsoft.AspNetCore.Cors.Infrastructure.CorsOptions;

namespace Mlm.Api.Infrastructure;

internal static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddMlmInfrastructure(
        this IServiceCollection services,
        IHostEnvironment environment)
    {
        services.AddMlmOptions();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddSingleton(TimeProvider.System);

        services.AddSingleton<AuthCookieFactory>();
        services.AddSingleton<JwtTokenIssuer>();
        services.AddSingleton<PasswordHasher>();
        services.AddSingleton<IFieldEncryption, DataProtectionFieldEncryption>();
        services.AddScoped<AuthSessionService>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserAccessor, HttpCurrentUserAccessor>();
        services.AddScoped<AuditableSaveChangesInterceptor>();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var connection = sp.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
            options.UseNpgsql(connection.Default, npgsql =>
                npgsql.MigrationsHistoryTable("__ef_migrations_history"));
            options.AddInterceptors(sp.GetRequiredService<AuditableSaveChangesInterceptor>());
        });

        services.AddDbContext<QuartzDbContext>((sp, options) =>
        {
            var connection = sp.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
            options.UseNpgsql(connection.QuartzOrDefault, npgsql =>
                npgsql.MigrationsHistoryTable("__ef_migrations_history_quartz"));
        });

        services.AddScoped<DbMigrator>();
        services.AddScoped<SeedDbContext>();

        services.AddDataProtection()
            .PersistKeysToDbContext<AppDbContext>()
            .SetApplicationName("mlm-api")
            .SetDefaultKeyLifetime(TimeSpan.FromDays(90));

        services.AddJwtCookieAuth();
        services.AddAlmatQuartz();
        services.AddHealthChecks();
        services.AddMlmCors();

        if (!environment.IsDevelopment())
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor
                    | ForwardedHeaders.XForwardedProto
                    | ForwardedHeaders.XForwardedHost;
                options.KnownIPNetworks.Clear();
                options.KnownProxies.Clear();
            });
        }

        return services;
    }

    private static void AddMlmOptions(this IServiceCollection services)
    {
        services.AddOptions<ConnectionStringsOptions>()
            .BindConfiguration(ConnectionStringsOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<AuthCookiesOptions>()
            .BindConfiguration(AuthCookiesOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<WebCorsOptions>()
            .BindConfiguration(WebCorsOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<OpenApiOptions>()
            .BindConfiguration(OpenApiOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    private static void AddMlmCors(this IServiceCollection services)
    {
        services.AddCors();
        services.AddOptions<MsCors>()
            .Configure<IOptions<WebCorsOptions>>((cors, web) =>
            {
                cors.AddPolicy(WebCorsOptions.PolicyName, policy =>
                    policy.WithOrigins(web.Value.Origins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
    }
}
