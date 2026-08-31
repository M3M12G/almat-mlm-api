using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;

namespace Mlm.Api.Modules.Ranks.Entities;

internal sealed class RankConfiguration : IEntityTypeConfiguration<Rank>
{
    public void Configure(EntityTypeBuilder<Rank> builder)
    {
        builder.ToSnakeTable("ranks");
        builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
        builder.Property(r => r.SortOrder).HasColumnName("order");
        builder.Property(r => r.RequiredConditionJson).HasColumnType("jsonb").IsRequired();
        builder.Property(r => r.OneTimeBonus).HasPrecision(18, 2);
    }
}
