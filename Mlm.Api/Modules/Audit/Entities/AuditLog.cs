namespace Mlm.Api.Modules.Audit.Entities;

internal sealed class AuditLog
{
    public Guid Id { get; set; }
    public Guid? ActorId { get; set; }
    public string Action { get; set; } = "";
    public string EntityType { get; set; } = "";
    public string EntityKey { get; set; } = "";
    public string? PropertyName { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public Guid? OperationId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
