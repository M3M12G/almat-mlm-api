using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;

namespace Mlm.Api.Modules.BonusEngine.Entities;

internal sealed class BonusRuleConfiguration : IEntityTypeConfiguration<BonusRule>
{
    public void Configure(EntityTypeBuilder<BonusRule> builder)
    {
        builder.ToSnakeTable("bonus_rules");
        builder.Property(r => r.Code).HasMaxLength(64).IsRequired();
        builder.Property(r => r.Type).HasMaxLength(32).IsRequired();
        builder.Property(r => r.ConfigJson).HasColumnType("jsonb").IsRequired();
        builder.Property(r => r.ActiveFrom).HasDefaultValueSql("now()");
        builder.HasAlternateKey(r => r.Code);
    }
}
