using Microservice.Interface;

namespace Microservice.Services;

public interface IEmailClient
{
    Task SendAsync(SendEmailRequest request);
}