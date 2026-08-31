using Microsoft.Extensions.Options;

namespace Mlm.Api.Infrastructure.Auth;

internal sealed class AuthCookieFactory(
    IOptions<JwtOptions> jwt,
    IOptions<AuthCookiesOptions> cookies,
    IHostEnvironment environment)
{
    public CookieOptions AccessToken() => TokenCookie(TimeSpan.FromMinutes(jwt.Value.AccessTokenMinutes));

    public CookieOptions RefreshToken() => TokenCookie(TimeSpan.FromDays(jwt.Value.RefreshTokenDays));

    public CookieOptions DeviceId()
    {
        var options = Base();
        options.Expires = DateTimeOffset.UtcNow.AddDays(cookies.Value.DeviceIdDays);
        options.IsEssential = true;
        return options;
    }

    public CookieOptions DeleteTokens() => Base();

    private CookieOptions TokenCookie(TimeSpan lifetime)
    {
        var options = Base();
        options.Expires = DateTimeOffset.UtcNow.Add(lifetime);
        return options;
    }

    private CookieOptions Base()
    {
        var isDev = environment.IsDevelopment();
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = !isDev,
            SameSite = isDev ? SameSiteMode.Lax : SameSiteMode.None,
            Path = "/",
        };
    }
}
