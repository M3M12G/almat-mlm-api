namespace Mlm.Api.Infrastructure.Time;

internal static class AlmatyTimeZone
{
    public static TimeZoneInfo Instance { get; } = Resolve();

    private static TimeZoneInfo Resolve()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Asia/Almaty");
        }
        catch (TimeZoneNotFoundException)
        {
            return Fallback();
        }
        catch (InvalidTimeZoneException)
        {
            return Fallback();
        }
    }

    private static TimeZoneInfo Fallback() =>
        TimeZoneInfo.CreateCustomTimeZone(
            "Asia/Almaty",
            TimeSpan.FromHours(5),
            "Asia/Almaty",
            "Asia/Almaty");
}
