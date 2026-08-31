using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;

namespace Mlm.Api.Modules.LeadershipPool.Entities;

internal sealed class PoolPeriodConfiguration : IEntityTypeConfiguration<PoolPeriod>
{
    public void Configure(EntityTypeBuilder<PoolPeriod> builder)
    {
        builder.ToSnakeTable("pool_periods");
        builder.Property(p => p.Period).HasMaxLength(7).IsRequired();
        builder.Property(p => p.WorldTurnover).HasPrecision(18, 2);
        builder.Property(p => p.PoolAmount).HasPrecision(18, 2);
        builder.HasIndex(p => p.Period).IsUnique();
    }
}
