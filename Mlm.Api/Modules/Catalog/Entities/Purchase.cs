using Mlm.Api.Data;

namespace Mlm.Api.Modules.Catalog.Entities;

internal sealed class Purchase : AuditableEntity
{
    public Guid BuyerId { get; set; }
    public Guid? PackageId { get; set; }
    public decimal Amount { get; set; }
    public decimal Lp { get; set; }
    public string? PaymentProviderTxId { get; set; }
    public string Status { get; set; } = "pending";
}
