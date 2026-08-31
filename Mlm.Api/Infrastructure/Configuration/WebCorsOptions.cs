using System.ComponentModel.DataAnnotations;

namespace Mlm.Api.Infrastructure.Configuration;

internal sealed class WebCorsOptions
{
    public const string SectionName = "Cors";
    public const string PolicyName = "Web";

    [Required]
    [MinLength(1)]
    public required string[] Origins { get; init; }
}
