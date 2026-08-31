using Microsoft.EntityFrameworkCore;

namespace Mlm.Api.Data;

/// <summary>
/// Hosts Quartz JobStore tables. No CLR entities — vendor DDL is applied
/// by the Initial migration (ADR-0005).
/// </summary>
public sealed class QuartzDbContext(DbContextOptions<QuartzDbContext> options) : DbContext(options)
{
}
