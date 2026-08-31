using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace Mlm.Api.Infrastructure.Encryption;

internal sealed class DataProtectionFieldEncryption(IDataProtectionProvider provider) : IFieldEncryption
{
    public const string Prefix = "enc:v1:";

    private readonly IDataProtector protector = provider.CreateProtector("Mlm.Api.Pii.v1");

    public string Protect(string plainText)
    {
        if (string.IsNullOrEmpty(plainText) || IsProtected(plainText))
        {
            return plainText;
        }

        var bytes = protector.Protect(Encoding.UTF8.GetBytes(plainText));
        return Prefix + Convert.ToBase64String(bytes);
    }

    public string Unprotect(string stored)
    {
        if (string.IsNullOrEmpty(stored) || !IsProtected(stored))
        {
            return stored;
        }

        try
        {
            var bytes = Convert.FromBase64String(stored[Prefix.Length..]);
            return Encoding.UTF8.GetString(protector.Unprotect(bytes));
        }
        catch (Exception ex) when (ex is FormatException or CryptographicException)
        {
            throw new CryptographicException("Failed to unprotect field.", ex);
        }
    }

    public bool IsProtected(string value) =>
        value.StartsWith(Prefix, StringComparison.Ordinal);
}
