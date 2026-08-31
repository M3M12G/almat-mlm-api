using Mlm.Api.Data;

namespace Mlm.Api.Modules.Identity.Entities;

internal sealed class AuthSession : AuditableEntity
{
    public Guid UserId { get; set; }
    public string RefreshTokenHash { get; set; } = "";
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public string? UserAgent { get; set; }
    public string? IpAddress { get; set; }
    public string? DeviceId { get; set; }
}
