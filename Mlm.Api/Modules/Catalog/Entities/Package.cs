using Mlm.Api.Data;

namespace Mlm.Api.Modules.Catalog.Entities;

internal sealed class Package : AuditableEntity
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public decimal LpValue { get; set; }
    public string? Description { get; set; }
}
