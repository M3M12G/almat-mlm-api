using Mlm.Api.Data;

namespace Mlm.Api.Modules.Ranks.Entities;

internal sealed class Rank : AuditableEntity
{
    public string Name { get; set; } = "";
    public int SortOrder { get; set; }
    public string RequiredConditionJson { get; set; } = "{}";
    public decimal OneTimeBonus { get; set; }
    public int LeadershipPoolPoints { get; set; }
}
