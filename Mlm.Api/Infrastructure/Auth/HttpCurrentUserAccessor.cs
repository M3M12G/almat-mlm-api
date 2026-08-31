using System.Security.Claims;

namespace Mlm.Api.Infrastructure.Auth;

internal sealed class HttpCurrentUserAccessor(IHttpContextAccessor http) : ICurrentUserAccessor
{
    public Guid? UserId
    {
        get
        {
            var value = http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(value, out var id) ? id : null;
        }
    }
}
