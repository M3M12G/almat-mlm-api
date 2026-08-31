using System.ComponentModel.DataAnnotations;

namespace Mlm.Api.Infrastructure.Auth;

internal sealed class AuthCookiesOptions
{
    public const string SectionName = "AuthCookies";

    [Range(1, 3650)]
    public int DeviceIdDays { get; init; } = 365;
}
