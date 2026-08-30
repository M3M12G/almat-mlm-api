using Microsoft.EntityFrameworkCore;

namespace AlmatMlm.Api.Data;

/// <summary>
/// Application DbContext. Domain entities arrive in later tickets.
/// TickerQ tables are injected via UseApplicationDbContext model customizer.
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Domain entity configs — later tickets. Initial schema applied via SQL migration.
    }
}
