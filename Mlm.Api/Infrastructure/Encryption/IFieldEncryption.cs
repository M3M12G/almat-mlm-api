namespace Mlm.Api.Infrastructure.Encryption;

internal interface IFieldEncryption
{
    string Protect(string plainText);
    string Unprotect(string stored);
    bool IsProtected(string value);
}
