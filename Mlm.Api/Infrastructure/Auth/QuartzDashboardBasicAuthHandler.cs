using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Mlm.Api.Infrastructure.Auth;

internal sealed class QuartzDashboardBasicAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IOptions<QuartzDashboardAuthOptions> credentials)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!AuthenticationHeaderValue.TryParse(Request.Headers.Authorization, out var header)
            || !string.Equals(header.Scheme, "Basic", StringComparison.OrdinalIgnoreCase)
            || string.IsNullOrEmpty(header.Parameter))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (!TryDecode(header.Parameter, out var username, out var password))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header."));
        }

        var expected = credentials.Value;
        if (!FixedTimeEquals(username, expected.Username)
            || !FixedTimeEquals(password, expected.Password))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid credentials."));
        }

        Claim[] claims =
        [
            new(ClaimTypes.NameIdentifier, username),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, QuartzDashboardAuthOptions.Role),
        ];

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.Headers.WWWAuthenticate = "Basic realm=\"Quartz Dashboard\", charset=\"UTF-8\"";
        return base.HandleChallengeAsync(properties);
    }

    private static bool TryDecode(string parameter, out string username, out string password)
    {
        username = string.Empty;
        password = string.Empty;

        try
        {
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(parameter));
            var separator = decoded.IndexOf(':');
            if (separator < 0)
            {
                return false;
            }

            username = decoded[..separator];
            password = decoded[(separator + 1)..];
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private static bool FixedTimeEquals(string a, string b)
    {
        var left = SHA256.HashData(Encoding.UTF8.GetBytes(a));
        var right = SHA256.HashData(Encoding.UTF8.GetBytes(b));
        return CryptographicOperations.FixedTimeEquals(left, right);
    }
}
