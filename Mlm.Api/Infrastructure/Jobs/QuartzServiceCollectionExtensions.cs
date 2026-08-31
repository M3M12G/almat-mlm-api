using Microsoft.Extensions.Options;
using Mlm.Api.Infrastructure.Auth;
using Mlm.Api.Infrastructure.Configuration;
using Mlm.Api.Infrastructure.Time;
using Quartz;

namespace Mlm.Api.Infrastructure.Jobs;

internal static class QuartzServiceCollectionExtensions
{
    public static IServiceCollection AddAlmatQuartz(this IServiceCollection services)
    {
        services.AddQuartzDashboardAuth();

        services.AddQuartz((q, sp) =>
        {
            var connectionString = sp.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value.QuartzOrDefault;

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

            var poolKey = new JobKey("LeadershipPoolJob", "bonus");
            q.AddJob<LeadershipPoolJob>(opts => opts
                .WithIdentity(poolKey)
                .StoreDurably()
                .WithDescription("Monthly Leadership Pool (2% world TO → Gold Director+)"));

            q.AddTrigger(opts => opts
                .ForJob(poolKey)
                .WithIdentity("LeadershipPoolMonthly", "bonus")
                .WithDescription("1st of month 00:00 Asia/Almaty")
                .WithCronSchedule(
                    "0 0 0 1 * ?",
                    x => x.InTimeZone(AlmatyTimeZone.Instance)));

            var cleanupKey = new JobKey("CleanupExpiredAuthSessionsJob", "auth");
            q.AddJob<CleanupExpiredAuthSessionsJob>(opts => opts
                .WithIdentity(cleanupKey)
                .StoreDurably()
                .WithDescription("Delete expired and revoked auth sessions"));

            q.AddTrigger(opts => opts
                .ForJob(cleanupKey)
                .WithIdentity("CleanupExpiredAuthSessionsDaily", "auth")
                .WithDescription("03:00 Asia/Almaty")
                .WithCronSchedule(
                    "0 0 3 * * ?",
                    x => x.InTimeZone(AlmatyTimeZone.Instance)));
        });

        services.AddQuartzDashboard(options =>
        {
            options.AuthorizationPolicy = QuartzDashboardAuthOptions.PolicyName;
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }
}
