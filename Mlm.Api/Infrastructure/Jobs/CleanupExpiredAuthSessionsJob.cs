using Mlm.Api.Modules.Identity.Services;
using Quartz;

namespace Mlm.Api.Infrastructure.Jobs;

[DisallowConcurrentExecution]
internal sealed class CleanupExpiredAuthSessionsJob(AuthSessionService sessions) : IJob
{
    public Task Execute(IJobExecutionContext context) =>
        sessions.DeleteExpiredAsync(context.CancellationToken);
}
