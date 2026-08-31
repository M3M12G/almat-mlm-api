using System.ComponentModel.DataAnnotations;

namespace Mlm.Api.Infrastructure.Auth;

internal sealed class QuartzDashboardAuthOptions
{
    public const string SectionName = "Quartz:Dashboard";
    public const string PolicyName = "QuartzDashboard";
    public const string AuthenticationScheme = "QuartzDashboardBasic";
    public const string Role = "SchedulerAdmin";

    [Required]
    public required string Username { get; init; }

    [Required]
    public required string Password { get; init; }
}
