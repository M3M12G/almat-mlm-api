using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace Mlm.Api.Infrastructure.Auth;

/// <summary>
/// Protects CrystalQuartz UI (/quartz) with HTTP Basic Auth.
/// Credentials: Quartz:Dashboard:Username / Password (required every env).
/// </summary>
public sealed class QuartzDashboardBasicAuthMiddleware(
    RequestDelegate next,
    IConfiguration configuration,
    ILogger<QuartzDashboardBasicAuthMiddleware> logger)
{
    public const string PathPrefix = "/quartz";

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Path.StartsWithSegments(PathPrefix, StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }

        var expectedUser = configuration["Quartz:Dashboard:Username"] ?? "admin";
        var expectedPassword = configuration["Quartz:Dashboard:Password"];
        if (string.IsNullOrWhiteSpace(expectedPassword))
        {
            logger.LogError("Quartz:Dashboard:Password is not set — refusing dashboard access");
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Quartz dashboard is not configured.");
            return;
        }

        if (!TryGetBasicCredentials(context.Request, out var username, out var password)
            || !FixedTimeEquals(username, expectedUser)
            || !FixedTimeEquals(password, expectedPassword))
        {
            context.Response.Headers.WWWAuthenticate = "Basic realm=\"Quartz Dashboard\"";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await next(context);
    }

    private static bool TryGetBasicCredentials(HttpRequest request, out string username, out string password)
    {
        username = string.Empty;
        password = string.Empty;

        if (!request.Headers.TryGetValue("Authorization", out var headerValues))
        {
            return false;
        }

        var header = AuthenticationHeaderValue.Parse(headerValues.ToString());
        if (!string.Equals(header.Scheme, "Basic", StringComparison.OrdinalIgnoreCase)
            || string.IsNullOrEmpty(header.Parameter))
        {
            return false;
        }

        try
        {
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(header.Parameter));
            var parts = decoded.Split(':', 2);
            username = parts[0];
            password = parts.Length > 1 ? parts[1] : string.Empty;
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static bool FixedTimeEquals(string a, string b)
    {
        var ba = Encoding.UTF8.GetBytes(a);
        var bb = Encoding.UTF8.GetBytes(b);
        return ba.Length == bb.Length && CryptographicOperations.FixedTimeEquals(ba, bb);
    }
}
