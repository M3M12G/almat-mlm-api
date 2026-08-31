using Mlm.Api.Data;

namespace Mlm.Api.Modules.BonusEngine.Entities;

internal sealed class BonusTransaction : AuditableEntity
{
    public string RuleCode { get; set; } = "";
    public Guid? SourcePurchaseId { get; set; }
    public Guid? FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public int? Level { get; set; }
    public decimal Amount { get; set; }
    public string? Period { get; set; }
    public string Status { get; set; } = "accrued";
}
