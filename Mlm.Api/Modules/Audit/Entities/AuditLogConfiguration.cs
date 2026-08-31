using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;
using Mlm.Api.Modules.Identity.Entities;

namespace Mlm.Api.Modules.Audit.Entities;

internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToSnakeTable("audit_log");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("gen_random_uuid()");
        builder.Property(a => a.Action).HasMaxLength(16).IsRequired();
        builder.Property(a => a.EntityType).HasMaxLength(64).IsRequired();
        builder.Property(a => a.EntityKey).HasMaxLength(64).IsRequired();
        builder.Property(a => a.PropertyName).HasMaxLength(128);
        builder.Property(a => a.OldValue).HasMaxLength(4000);
        builder.Property(a => a.NewValue).HasMaxLength(4000);
        builder.Property(a => a.CreatedAt).HasDefaultValueSql("now()");

        builder.HasIndex(a => new { a.EntityType, a.EntityKey });
        builder.HasIndex(a => a.ActorId);
        builder.HasIndex(a => a.CreatedAt);
        builder.HasIndex(a => a.OperationId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.ActorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
