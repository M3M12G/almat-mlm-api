using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;
using Mlm.Api.Modules.Catalog.Entities;
using Mlm.Api.Modules.Identity.Entities;

namespace Mlm.Api.Modules.BonusEngine.Entities;

internal sealed class BonusTransactionConfiguration : IEntityTypeConfiguration<BonusTransaction>
{
    public void Configure(EntityTypeBuilder<BonusTransaction> builder)
    {
        builder.ToSnakeTable("bonus_transactions");
        builder.Property(t => t.RuleCode).HasMaxLength(64).IsRequired();
        builder.Property(t => t.Amount).HasPrecision(18, 2);
        builder.Property(t => t.Period).HasMaxLength(7);
        builder.Property(t => t.Status).HasMaxLength(32).IsRequired();

        builder.HasIndex(t => t.ToUserId);
        builder.HasIndex(t => t.SourcePurchaseId);
        builder.HasIndex(t => t.Period);

        builder.HasOne<BonusRule>()
            .WithMany()
            .HasForeignKey(t => t.RuleCode)
            .HasPrincipalKey(r => r.Code)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Purchase>()
            .WithMany()
            .HasForeignKey(t => t.SourcePurchaseId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.FromUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.ToUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
