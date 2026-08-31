using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;
using Mlm.Api.Modules.Identity.Entities;

namespace Mlm.Api.Modules.Ranks.Entities;

internal sealed class RankAchievementConfiguration : IEntityTypeConfiguration<RankAchievement>
{
    public void Configure(EntityTypeBuilder<RankAchievement> builder)
    {
        builder.ToSnakeTable("rank_achievements");
        builder.Property(a => a.AchievedAt).HasDefaultValueSql("now()");

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Rank>()
            .WithMany()
            .HasForeignKey(a => a.RankId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
