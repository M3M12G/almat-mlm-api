using Mlm.Api.Data;

namespace Mlm.Api.Modules.Accounting.Entities;

internal sealed class AccountingEntry : AuditableEntity
{
    public string EntryType { get; set; } = "";
    public decimal Amount { get; set; }
    public string SourceType { get; set; } = "";
    public Guid SourceId { get; set; }
}
