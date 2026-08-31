using System.Globalization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Mlm.Api.Data;
using Mlm.Api.Infrastructure.Auth;
using Mlm.Api.Infrastructure.Encryption;
using Mlm.Api.Modules.Audit.Entities;
using Mlm.Api.Modules.Identity.Entities;

namespace Mlm.Api.Modules.Audit;

internal sealed class AuditableSaveChangesInterceptor(
    ICurrentUserAccessor currentUser,
    TimeProvider clock) : SaveChangesInterceptor
{
    private const int ValueLimit = 4000;

    private static readonly HashSet<string> SkipPropertyNames =
    [
        nameof(AuditableEntity.CreatedAt),
        nameof(AuditableEntity.CreatedBy),
        nameof(AuditableEntity.UpdatedAt),
        nameof(AuditableEntity.UpdatedBy),
        nameof(User.PasswordHash),
        nameof(User.Iin),
        nameof(AuthSession.RefreshTokenHash),
        nameof(AuthSession.IpAddress),
    ];

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        Apply(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        Apply(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void Apply(DbContext? context)
    {
        if (context is not AppDbContext db)
        {
            return;
        }

        var actorId = currentUser.UserId;
        var now = clock.GetUtcNow();
        var operationId = Guid.NewGuid();
        var logs = new List<AuditLog>();

        var entries = db.ChangeTracker.Entries()
            .Where(e => e.Entity is not AuditLog)
            .ToList();

        foreach (var entry in entries)
        {
            if (entry.Entity is AuditableEntity auditable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditable.CreatedAt = now;
                        auditable.CreatedBy = actorId;
                        break;
                    case EntityState.Modified:
                        auditable.UpdatedAt = now;
                        auditable.UpdatedBy = actorId;
                        break;
                }
            }

            if (entry.State == EntityState.Modified)
            {
                foreach (var property in entry.Properties.Where(ShouldLogColumn))
                {
                    logs.Add(CreateLog(entry, actorId, now, operationId, "update", property));
                }
            }
            else if (entry.State == EntityState.Deleted)
            {
                logs.Add(CreateLog(entry, actorId, now, operationId, "delete", property: null));
            }
        }

        if (logs.Count > 0)
        {
            db.AuditLogs.AddRange(logs);
        }
    }

    private static bool ShouldLogColumn(PropertyEntry property)
    {
        if (!property.IsModified || property.Metadata.IsPrimaryKey() || property.Metadata.IsShadowProperty())
        {
            return false;
        }

        if (SkipPropertyNames.Contains(property.Metadata.Name))
        {
            return false;
        }

        var clr = property.Metadata.PropertyInfo;
        if (clr is not null && Attribute.IsDefined(clr, typeof(EncryptedAttribute)))
        {
            return false;
        }

        var original = FormatValue(property.OriginalValue);
        var current = FormatValue(property.CurrentValue);
        return !string.Equals(original, current, StringComparison.Ordinal);
    }

    private static AuditLog CreateLog(
        EntityEntry entry,
        Guid? actorId,
        DateTimeOffset now,
        Guid operationId,
        string action,
        PropertyEntry? property)
    {
        return new AuditLog
        {
            ActorId = actorId,
            Action = action,
            EntityType = entry.Metadata.ClrType.Name,
            EntityKey = GetKey(entry),
            PropertyName = property?.Metadata.Name,
            OldValue = property is null ? null : FormatValue(property.OriginalValue),
            NewValue = property is null ? null : FormatValue(property.CurrentValue),
            OperationId = operationId,
            CreatedAt = now,
        };
    }

    private static string GetKey(EntityEntry entry)
    {
        var parts = entry.Properties
            .Where(p => p.Metadata.IsPrimaryKey())
            .Select(p => (p.CurrentValue ?? p.OriginalValue)?.ToString() ?? "")
            .ToArray();
        return string.Join(',', parts);
    }

    private static string? FormatValue(object? value)
    {
        if (value is null)
        {
            return null;
        }

        var text = value switch
        {
            string s => s,
            DateTimeOffset dto => dto.ToString("O", CultureInfo.InvariantCulture),
            DateTime dt => DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToString("O", CultureInfo.InvariantCulture),
            IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
            _ => JsonSerializer.Serialize(value),
        };

        return text.Length <= ValueLimit ? text : text[..ValueLimit];
    }
}
