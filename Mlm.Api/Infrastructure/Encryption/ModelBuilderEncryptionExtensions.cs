using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mlm.Api.Infrastructure.Encryption;

internal static class ModelBuilderEncryptionExtensions
{
    public static void ApplyEncryptedConverters(this ModelBuilder modelBuilder, IFieldEncryption encryption)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.ClrType.GetProperties())
            {
                if (property.GetCustomAttribute<EncryptedAttribute>() is null)
                {
                    continue;
                }

                var converterType = typeof(EncryptionValueConverter<>).MakeGenericType(property.PropertyType);
                var converter = (ValueConverter)Activator.CreateInstance(
                    converterType,
                    encryption,
                    $"{entityType.ClrType.Name}.{property.Name}")!;

                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasConversion(converter);
            }
        }
    }
}
