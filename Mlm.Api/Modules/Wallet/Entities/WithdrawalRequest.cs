using Mlm.Api.Data;

namespace Mlm.Api.Modules.Wallet.Entities;

internal sealed class WithdrawalRequest : AuditableEntity
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = "pending";
    public DateTimeOffset RequestedAt { get; set; }
    public DateTimeOffset? ProcessedAt { get; set; }
    public Guid? ProcessedBy { get; set; }
}
