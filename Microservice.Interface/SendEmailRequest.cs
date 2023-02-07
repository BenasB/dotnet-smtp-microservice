using System.ComponentModel.DataAnnotations;

namespace Microservice.Interface;

public record SendEmailRequest
{
    [Required]
    [EmailAddress]
    public string From { get; init; } = null!;

    [Required]
    [EmailAddress]
    public string To { get; init; } = null!;

    [Required]
    public string Subject { get; init; } = null!;

    [Required]
    public string Body { get; init; } = null!;
}