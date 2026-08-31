using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mlm.Api.Data;

namespace Mlm.Api.Modules.Identity.Entities;

internal sealed class AuthSessionConfiguration : IEntityTypeConfiguration<AuthSession>
{
    public void Configure(EntityTypeBuilder<AuthSession> builder)
    {
        builder.ToSnakeTable("auth_sessions");
        builder.Property(s => s.RefreshTokenHash).HasMaxLength(64).IsRequired();
        builder.Property(s => s.UserAgent).HasMaxLength(512);
        builder.Property(s => s.IpAddress).HasMaxLength(64);
        builder.Property(s => s.DeviceId).HasMaxLength(32);

        builder.HasIndex(s => s.RefreshTokenHash).IsUnique();
        builder.HasIndex(s => s.UserId);
        builder.HasIndex(s => s.ExpiresAt);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
