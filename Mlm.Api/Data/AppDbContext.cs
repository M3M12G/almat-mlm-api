using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Mlm.Api.Infrastructure.Encryption;
using Mlm.Api.Modules.Accounting.Entities;
using Mlm.Api.Modules.Audit.Entities;
using Mlm.Api.Modules.BonusEngine.Entities;
using Mlm.Api.Modules.Catalog.Entities;
using Mlm.Api.Modules.Identity.Entities;
using Mlm.Api.Modules.LeadershipPool.Entities;
using Mlm.Api.Modules.Ranks.Entities;
using Mlm.Api.Modules.Wallet.Entities;

namespace Mlm.Api.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IDataProtectionKeyContext
{
    internal DbSet<User> Users => Set<User>();
    internal DbSet<AuthSession> AuthSessions => Set<AuthSession>();
    internal DbSet<Rank> Ranks => Set<Rank>();
    internal DbSet<RankAchievement> RankAchievements => Set<RankAchievement>();
    internal DbSet<Package> Packages => Set<Package>();
    internal DbSet<Purchase> Purchases => Set<Purchase>();
    internal DbSet<BonusRule> BonusRules => Set<BonusRule>();
    internal DbSet<BonusTransaction> BonusTransactions => Set<BonusTransaction>();
    internal DbSet<PoolPeriod> PoolPeriods => Set<PoolPeriod>();
    internal DbSet<PoolDistribution> PoolDistributions => Set<PoolDistribution>();
    internal DbSet<WithdrawalRequest> WithdrawalRequests => Set<WithdrawalRequest>();
    internal DbSet<AccountingEntry> AccountingEntries => Set<AccountingEntry>();
    internal DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        EntityMapping.ConfigureAuditable(modelBuilder);

        modelBuilder.Entity<DataProtectionKey>(b =>
        {
            b.ToTable("data_protection_keys");
            b.Property(e => e.Id).HasColumnName("id");
            b.Property(e => e.FriendlyName).HasColumnName("friendly_name");
            b.Property(e => e.Xml).HasColumnName("xml");
        });

        IFieldEncryption? encryption = null;
        try
        {
            encryption = this.GetService<IFieldEncryption>();
        }
        catch (InvalidOperationException)
        {
            // Design-time factory has no encryption service; store type stays text.
        }

        if (encryption is not null)
        {
            modelBuilder.ApplyEncryptedConverters(encryption);
        }
    }
}
