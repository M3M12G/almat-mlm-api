using Microsoft.EntityFrameworkCore;

namespace Mlm.Api.Data;

/// <summary>
/// Application DbContext. Domain entities arrive in later tickets.
/// Quartz JobStore tables are managed via SQL script (not EF entities).
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Domain entity configs — later tickets. Initial schema applied via SQL migration.
    }
}
