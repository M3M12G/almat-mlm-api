using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mlm.Api.Data;

internal static class EntityMapping
{
    public static void ToSnakeTable<T>(this EntityTypeBuilder<T> builder, string table)
        where T : class
    {
        builder.ToTable(table);
        foreach (var property in builder.Metadata.GetProperties())
        {
            property.SetColumnName(SnakeCase.FromPascal(property.Name));
        }
    }

    public static void ConfigureAuditable(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                continue;
            }

            var entity = modelBuilder.Entity(entityType.ClrType);
            entity.HasKey(nameof(AuditableEntity.Id));
            entity.Property(nameof(AuditableEntity.Id)).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(nameof(AuditableEntity.CreatedAt)).HasDefaultValueSql("now()");
        }
    }
}
