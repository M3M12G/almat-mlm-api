using Microsoft.AspNetCore.Identity;

namespace Mlm.Api.Infrastructure.Auth;

internal sealed class PasswordHasher
{
    private static readonly object Dummy = new();
    private readonly PasswordHasher<object> inner = new();

    public string Hash(string password) => inner.HashPassword(Dummy, password);

    public bool Verify(string hash, string password) =>
        inner.VerifyHashedPassword(Dummy, hash, password)
            is PasswordVerificationResult.Success
            or PasswordVerificationResult.SuccessRehashNeeded;
}
