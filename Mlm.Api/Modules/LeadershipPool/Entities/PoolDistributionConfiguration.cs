using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;
using Mlm.Api.Modules.Identity.Entities;
using Mlm.Api.Modules.Ranks.Entities;

namespace Mlm.Api.Modules.LeadershipPool.Entities;

internal sealed class PoolDistributionConfiguration : IEntityTypeConfiguration<PoolDistribution>
{
    public void Configure(EntityTypeBuilder<PoolDistribution> builder)
    {
        builder.ToSnakeTable("pool_distributions");
        builder.Property(d => d.Amount).HasPrecision(18, 2);

        builder.HasOne<PoolPeriod>()
            .WithMany()
            .HasForeignKey(d => d.PeriodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Rank>()
            .WithMany()
            .HasForeignKey(d => d.RankId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
