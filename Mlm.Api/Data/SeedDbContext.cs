using Microsoft.EntityFrameworkCore;
using Mlm.Api.Modules.Catalog.Entities;
using Mlm.Api.Modules.Ranks.Entities;

namespace Mlm.Api.Data;

internal sealed class SeedDbContext(ILogger<SeedDbContext> logger, AppDbContext db)
{
    private sealed record PackageSeed(string Name, decimal Price, string Description);

    private sealed record RankSeed(
        string Name,
        int SortOrder,
        decimal OneTimeBonus,
        int LeadershipPoolPoints);

    private static readonly PackageSeed[] Packages =
    [
        new("START", 18_000m, "Стартовый пакет"),
        new("BUSINESS", 54_000m, "Пакет для активного развития"),
        new("PREMIUM", 162_000m, "Лидерский пакет"),
    ];

    private static readonly RankSeed[] Ranks =
    [
        new("Консультант", 1, 20_000m, 0),
        new("Старший консультант", 2, 30_000m, 0),
        new("Менеджер", 3, 50_000m, 0),
        new("Старший менеджер", 4, 100_000m, 0),
        new("Директор", 5, 200_000m, 0),
        new("Серебряный директор", 6, 500_000m, 0),
        new("Бронзовый директор", 7, 1_000_000m, 0),
        new("Золотой директор", 8, 2_000_000m, 1),
        new("Платиновый директор", 9, 5_000_000m, 3),
        new("Бриллиантовый директор", 10, 10_000_000m, 7),
        new("Амбассадор", 11, 25_000_000m, 15),
    ];

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await SeedPackagesAsync(cancellationToken);
        await SeedRanksAsync(cancellationToken);
    }

    private async Task SeedPackagesAsync(CancellationToken cancellationToken)
    {
        var existing = (await db.Packages
            .Select(p => p.Name)
            .ToListAsync(cancellationToken))
            .ToHashSet(StringComparer.Ordinal);

        var missing = Packages
            .Where(p => !existing.Contains(p.Name))
            .Select(p => new Package
            {
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
            })
            .ToList();

        if (missing.Count == 0)
        {
            return;
        }

        db.Packages.AddRange(missing);
        await db.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Seeded {Count} packages", missing.Count);
    }

    private async Task SeedRanksAsync(CancellationToken cancellationToken)
    {
        var existing = (await db.Ranks
            .Select(r => r.Name)
            .ToListAsync(cancellationToken))
            .ToHashSet(StringComparer.Ordinal);

        var missing = Ranks
            .Where(r => !existing.Contains(r.Name))
            .Select(r => new Rank
            {
                Name = r.Name,
                SortOrder = r.SortOrder,
                OneTimeBonus = r.OneTimeBonus,
                LeadershipPoolPoints = r.LeadershipPoolPoints,
                RequiredConditionJson = "{}",
            })
            .ToList();

        if (missing.Count == 0)
        {
            return;
        }

        db.Ranks.AddRange(missing);
        await db.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Seeded {Count} ranks", missing.Count);
    }
}
