using Mlm.Api.Data;
using Mlm.Api.Infrastructure.Encryption;

namespace Mlm.Api.Modules.Identity.Entities;

internal sealed class User : AuditableEntity
{
    public Guid? SponsorId { get; set; }
    public string Email { get; set; } = "";
    public string? Phone { get; set; }
    public string ReferralCode { get; set; } = "";
    public Guid? RankId { get; set; }
    public decimal TotalTeamVolume { get; set; }
    public bool IsActivePeriod { get; set; }
    public string? PasswordHash { get; set; }

    [Encrypted]
    public string? Iin { get; set; }

    public int FailedLoginCount { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
}
