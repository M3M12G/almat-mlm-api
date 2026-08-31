using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;
using Mlm.Api.Modules.Identity.Entities;

namespace Mlm.Api.Modules.Catalog.Entities;

internal sealed class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToSnakeTable("purchases");
        builder.Property(p => p.Amount).HasPrecision(18, 2);
        builder.Property(p => p.Lp).HasPrecision(18, 2);
        builder.Property(p => p.PaymentProviderTxId).HasMaxLength(255);
        builder.Property(p => p.Status).HasMaxLength(32).IsRequired();

        builder.HasIndex(p => p.PaymentProviderTxId).IsUnique();
        builder.HasIndex(p => p.BuyerId);
        builder.HasIndex(p => p.CreatedAt);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Package>()
            .WithMany()
            .HasForeignKey(p => p.PackageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
