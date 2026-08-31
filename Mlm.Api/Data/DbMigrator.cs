using Microsoft.EntityFrameworkCore;

namespace Mlm.Api.Data;

internal sealed class DbMigrator(
    ILogger<DbMigrator> logger,
    AppDbContext db,
    QuartzDbContext quartzDb)
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Applying AppDbContext migrations");
        await db.Database.MigrateAsync(cancellationToken);

        logger.LogInformation("Applying QuartzDbContext migrations");
        await quartzDb.Database.MigrateAsync(cancellationToken);
    }
}
