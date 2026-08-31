using Quartz;

namespace Mlm.Api.Infrastructure.Jobs;

/// <summary>
/// Monthly Leadership Pool calculation (stub until bonus engine lands).
/// Cron: 1st of month 00:00 Asia/Almaty — configured in DI.
/// </summary>
[DisallowConcurrentExecution]
public sealed class LeadershipPoolJob(ILogger<LeadershipPoolJob> logger) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation(
            "LeadershipPoolJob fired at {FireTimeUtc} (stub — no calculation yet)",
            context.FireTimeUtc);
        return Task.CompletedTask;
    }
}
