namespace Mlm.Api.Data;

internal static class DatabaseInitializerExtensions
{
    public static async Task InitializeDatabaseAsync(
        this WebApplication app,
        CancellationToken cancellationToken = default)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var migrator = scope.ServiceProvider.GetRequiredService<DbMigrator>();
        await migrator.InitializeAsync(cancellationToken);

        var seeder = scope.ServiceProvider.GetRequiredService<SeedDbContext>();
        await seeder.SeedAsync(cancellationToken);
    }
}
