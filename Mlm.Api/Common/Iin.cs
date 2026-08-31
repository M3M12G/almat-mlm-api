namespace Mlm.Api.Common;

internal static class Iin
{
    public static bool IsValid(string? value) =>
        !string.IsNullOrWhiteSpace(value)
        && value.Length == 12
        && value.All(char.IsDigit);

    public static DateOnly? TryBirthDate(string? value)
    {
        if (value is null || !IsValid(value))
        {
            return null;
        }

        if (value.Distinct().Count() == 1 || value.StartsWith("123456", StringComparison.Ordinal))
        {
            return null;
        }

        var yearPart = int.Parse(value[..2]);
        var month = int.Parse(value[2..4]);
        var day = int.Parse(value[4..6]);
        var centuryCode = value[6] - '0';

        var century = centuryCode switch
        {
            1 or 2 => 1800,
            3 or 4 => 1900,
            5 or 6 => 2000,
            _ => -1,
        };

        if (century < 0)
        {
            return null;
        }

        try
        {
            return new DateOnly(century + yearPart, month, day);
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }
}
