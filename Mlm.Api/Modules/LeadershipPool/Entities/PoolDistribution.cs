using Mlm.Api.Data;

namespace Mlm.Api.Modules.LeadershipPool.Entities;

internal sealed class PoolDistribution : AuditableEntity
{
    public Guid PeriodId { get; set; }
    public Guid UserId { get; set; }
    public Guid RankId { get; set; }
    public int Points { get; set; }
    public decimal Amount { get; set; }
}
