using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mlm.Api.Infrastructure.Encryption;

internal sealed class EncryptionValueConverter<T> : ValueConverter<T?, string?>
{
    public EncryptionValueConverter(IFieldEncryption encryption, string fieldName)
        : base(
            v => v == null ? null : encryption.Protect(Serialize(v)),
            v => string.IsNullOrWhiteSpace(v) ? default : Deserialize(encryption.Unprotect(v), fieldName))
    {
    }

    private static readonly Type Underlying = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

    private static string Serialize(T value) => Underlying switch
    {
        Type t when t == typeof(string) => (string)(object)value!,
        Type t when t == typeof(Guid) => ((Guid)(object)value!).ToString("D"),
        Type t when t == typeof(DateOnly) => ((DateOnly)(object)value!).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
        _ => throw new NotSupportedException($"Encrypted type {Underlying.Name} is not supported."),
    };

    private static T Deserialize(string plaintext, string fieldName)
    {
        try
        {
            object parsed = Underlying switch
            {
                Type t when t == typeof(string) => plaintext,
                Type t when t == typeof(Guid) => Guid.Parse(plaintext),
                Type t when t == typeof(DateOnly) => DateOnly.Parse(plaintext, CultureInfo.InvariantCulture),
                _ => throw new NotSupportedException($"Encrypted type {Underlying.Name} is not supported."),
            };
            return (T)parsed;
        }
        catch (Exception ex) when (ex is FormatException or NotSupportedException)
        {
            throw new InvalidOperationException($"Failed to decode encrypted field '{fieldName}'.", ex);
        }
    }
}
