using Quartz;

namespace Mlm.Api.Infrastructure.Jobs;

internal static class QuartzServiceCollectionExtensions
{
    public static IServiceCollection AddAlmatQuartz(this IServiceCollection services, string connectionString)
    {
        services.AddQuartz(q =>
        {
            q.SchedulerName = "mlm-api";
            q.UseSimpleTypeLoader();
            q.UseDefaultThreadPool(tp => tp.MaxConcurrency = 5);

            q.UsePersistentStore(store =>
            {
                store.UseProperties = true;
                store.UsePostgres(postgres =>
                {
                    postgres.ConnectionString = connectionString;
                    postgres.TablePrefix = "qrtz_";
                });
                store.UseSystemTextJsonSerializer();
            });

            var jobKey = new JobKey("LeadershipPoolJob", "bonus");
            q.AddJob<LeadershipPoolJob>(opts => opts
                .WithIdentity(jobKey)
                .StoreDurably()
                .WithDescription("Monthly Leadership Pool (2% world TO → Gold Director+)"));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("LeadershipPoolMonthly", "bonus")
                .WithDescription("1st of month 00:00 Asia/Almaty")
                .WithCronSchedule(
                    "0 0 0 1 * ?",
                    x => x.InTimeZone(ResolveAlmatyTimeZone())));
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }

    private static TimeZoneInfo ResolveAlmatyTimeZone()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Asia/Almaty");
        }
        catch (TimeZoneNotFoundException)
        {
            return TimeZoneInfo.CreateCustomTimeZone(
                "Asia/Almaty",
                TimeSpan.FromHours(5),
                "Asia/Almaty",
                "Asia/Almaty");
        }
        catch (InvalidTimeZoneException)
        {
            return TimeZoneInfo.CreateCustomTimeZone(
                "Asia/Almaty",
                TimeSpan.FromHours(5),
                "Asia/Almaty",
                "Asia/Almaty");
        }
    }
}
