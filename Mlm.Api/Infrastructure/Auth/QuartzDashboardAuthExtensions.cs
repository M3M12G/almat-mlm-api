using Microsoft.AspNetCore.Authentication;

namespace Mlm.Api.Infrastructure.Auth;

internal static class QuartzDashboardAuthExtensions
{
    public static IServiceCollection AddQuartzDashboardAuth(this IServiceCollection services)
    {
        services.AddOptions<QuartzDashboardAuthOptions>()
            .BindConfiguration(QuartzDashboardAuthOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, QuartzDashboardBasicAuthHandler>(
                QuartzDashboardAuthOptions.AuthenticationScheme,
                _ => { });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(QuartzDashboardAuthOptions.PolicyName, policy =>
            {
                policy.AddAuthenticationSchemes(QuartzDashboardAuthOptions.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.RequireRole(QuartzDashboardAuthOptions.Role);
            });
        });

        return services;
    }
}
