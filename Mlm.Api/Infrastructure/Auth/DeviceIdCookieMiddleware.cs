namespace Mlm.Api.Infrastructure.Auth;

internal sealed class DeviceIdCookieMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, AuthCookieFactory cookies)
    {
        if (!context.Request.Cookies.ContainsKey(AuthCookieNames.DeviceId))
        {
            context.Response.Cookies.Append(
                AuthCookieNames.DeviceId,
                Guid.NewGuid().ToString("N"),
                cookies.DeviceId());
        }

        await next(context);
    }
}
