using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;

namespace Mlm.Api.Modules.Accounting.Entities;

internal sealed class AccountingEntryConfiguration : IEntityTypeConfiguration<AccountingEntry>
{
    public void Configure(EntityTypeBuilder<AccountingEntry> builder)
    {
        builder.ToSnakeTable("accounting_entries");
        builder.Property(e => e.EntryType).HasMaxLength(32).IsRequired();
        builder.Property(e => e.Amount).HasPrecision(18, 2);
        builder.Property(e => e.SourceType).HasMaxLength(32).IsRequired();
    }
}
