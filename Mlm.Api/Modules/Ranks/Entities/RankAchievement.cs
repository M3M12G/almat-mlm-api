using Mlm.Api.Data;

namespace Mlm.Api.Modules.Ranks.Entities;

internal sealed class RankAchievement : AuditableEntity
{
    public Guid UserId { get; set; }
    public Guid RankId { get; set; }
    public DateTimeOffset AchievedAt { get; set; }
    public bool BonusPaid { get; set; }
}
