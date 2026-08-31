using Mlm.Api.Data;

namespace Mlm.Api.Modules.LeadershipPool.Entities;

internal sealed class PoolPeriod : AuditableEntity
{
    public string Period { get; set; } = "";
    public decimal WorldTurnover { get; set; }
    public decimal PoolAmount { get; set; }
    public int TotalPoints { get; set; }
}
