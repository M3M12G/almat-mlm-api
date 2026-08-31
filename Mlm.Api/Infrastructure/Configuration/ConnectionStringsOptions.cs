using System.ComponentModel.DataAnnotations;

namespace Mlm.Api.Infrastructure.Configuration;

internal sealed class ConnectionStringsOptions
{
    public const string SectionName = "ConnectionStrings";

    [Required]
    public required string Default { get; init; }

    public string? Quartz { get; init; }

    public string QuartzOrDefault => string.IsNullOrWhiteSpace(Quartz) ? Default : Quartz;
}
