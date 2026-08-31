using System.ComponentModel.DataAnnotations;

namespace Mlm.Api.Infrastructure.Configuration;

internal sealed class OpenApiOptions
{
    public const string SectionName = "OpenApi";

    public bool Enabled { get; init; }

    [Required]
    public required string Title { get; init; }
}
