using MailKit.Net.Smtp;

namespace Microservice.Services;

public interface ISmtpClientFactory
{
    Task<ISmtpClient> CreateAsync();
}
