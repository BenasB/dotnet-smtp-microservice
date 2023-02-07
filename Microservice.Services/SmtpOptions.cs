namespace Microservice.Services;

public record SmtpOptions
{
    public string Host { get; set; } = null!;
    public int Port { get; set; } = 25;
}
