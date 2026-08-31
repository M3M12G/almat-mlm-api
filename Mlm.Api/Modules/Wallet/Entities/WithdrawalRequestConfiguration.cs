using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;
using Mlm.Api.Modules.Identity.Entities;

namespace Mlm.Api.Modules.Wallet.Entities;

internal sealed class WithdrawalRequestConfiguration : IEntityTypeConfiguration<WithdrawalRequest>
{
    public void Configure(EntityTypeBuilder<WithdrawalRequest> builder)
    {
        builder.ToSnakeTable("withdrawal_requests");
        builder.Property(w => w.Amount).HasPrecision(18, 2);
        builder.Property(w => w.Status).HasMaxLength(32).IsRequired();
        builder.Property(w => w.RequestedAt).HasDefaultValueSql("now()");

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(w => w.ProcessedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
