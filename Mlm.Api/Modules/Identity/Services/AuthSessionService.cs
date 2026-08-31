using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mlm.Api.Data;
using Mlm.Api.Infrastructure.Auth;
using Mlm.Api.Modules.Identity.Entities;

namespace Mlm.Api.Modules.Identity.Services;

internal sealed class AuthSessionService(
    AppDbContext db,
    TimeProvider clock,
    IOptions<JwtOptions> jwt)
{
    public async Task<string> CreateAsync(
        Guid userId,
        string? userAgent,
        string? ipAddress,
        string? deviceId,
        CancellationToken cancellationToken)
    {
        var raw = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        db.AuthSessions.Add(new AuthSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            RefreshTokenHash = Hash(raw),
            CreatedAt = clock.GetUtcNow(),
            ExpiresAt = clock.GetUtcNow().AddDays(jwt.Value.RefreshTokenDays),
            UserAgent = Truncate(userAgent, 512),
            IpAddress = Truncate(ipAddress, 64),
            DeviceId = Truncate(deviceId, 32),
        });
        await db.SaveChangesAsync(cancellationToken);
        return raw;
    }

    public async Task<AuthSession?> FindActiveAsync(string rawRefreshToken, CancellationToken cancellationToken)
    {
        var hash = Hash(rawRefreshToken);
        var now = clock.GetUtcNow();
        return await db.AuthSessions
            .SingleOrDefaultAsync(
                s => s.RefreshTokenHash == hash && s.RevokedAt == null && s.ExpiresAt > now,
                cancellationToken);
    }

    public async Task<string> RotateAsync(AuthSession session, CancellationToken cancellationToken)
    {
        session.RevokedAt = clock.GetUtcNow();
        return await CreateAsync(
            session.UserId,
            session.UserAgent,
            session.IpAddress,
            session.DeviceId,
            cancellationToken);
    }

    public async Task RevokeAsync(AuthSession session, CancellationToken cancellationToken)
    {
        session.RevokedAt = clock.GetUtcNow();
        await db.SaveChangesAsync(cancellationToken);
    }

    public Task<int> DeleteExpiredAsync(CancellationToken cancellationToken)
    {
        var now = clock.GetUtcNow();
        return db.AuthSessions
            .Where(s => s.ExpiresAt < now || s.RevokedAt != null)
            .ExecuteDeleteAsync(cancellationToken);
    }

    private static string Hash(string raw)
    {
        var bytes = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(raw));
        return Convert.ToHexString(bytes);
    }

    private static string? Truncate(string? value, int max) =>
        string.IsNullOrEmpty(value) ? value : value.Length <= max ? value : value[..max];
}
