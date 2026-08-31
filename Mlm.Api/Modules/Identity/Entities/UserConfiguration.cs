using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;
using Mlm.Api.Modules.Ranks.Entities;

namespace Mlm.Api.Modules.Identity.Entities;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToSnakeTable("users");
        builder.ToTable("users", t => t.HasCheckConstraint("no_self_sponsor", "id <> sponsor_id"));
        builder.Property(u => u.Email).HasMaxLength(255).IsRequired();
        builder.Property(u => u.Phone).HasMaxLength(32);
        builder.Property(u => u.ReferralCode).HasMaxLength(32).IsRequired();
        builder.Property(u => u.TotalTeamVolume).HasPrecision(18, 2);

        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.ReferralCode).IsUnique();
        builder.HasIndex(u => u.SponsorId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(u => u.SponsorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Rank>()
            .WithMany()
            .HasForeignKey(u => u.RankId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
