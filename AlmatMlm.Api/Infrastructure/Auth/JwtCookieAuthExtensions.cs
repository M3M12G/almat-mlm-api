using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AlmatMlm.Api.Infrastructure.Auth;

/// <summary>
/// JWT bearer wired to read token from httpOnly cookie. No real verification logic yet —
/// signing key is config-only stub for the skeleton.
/// </summary>
public static class JwtCookieAuthExtensions
{
    public const string AccessTokenCookieName = "almat_access_token";

    public static IServiceCollection AddJwtCookieAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");
        var signingKey = jwtSection["SigningKey"]
            ?? "DEV_ONLY_CHANGE_ME_almat_mlm_skeleton_signing_key_32+";

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"] ?? "almat-mlm-api",
                    ValidAudience = jwtSection["Audience"] ?? "almat-mlm-web",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                    ClockSkew = TimeSpan.FromMinutes(1),
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.TryGetValue(AccessTokenCookieName, out var token)
                            && !string.IsNullOrWhiteSpace(token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    },
                };
            });

        services.AddAuthorization();
        return services;
    }
}
