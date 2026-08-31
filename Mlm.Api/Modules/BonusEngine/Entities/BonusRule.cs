using Mlm.Api.Data;

namespace Mlm.Api.Modules.BonusEngine.Entities;

internal sealed class BonusRule : AuditableEntity
{
    public string Code { get; set; } = "";
    public string Type { get; set; } = "";
    public string ConfigJson { get; set; } = "{}";
    public DateTimeOffset ActiveFrom { get; set; }
    public DateTimeOffset? ActiveTo { get; set; }
}
