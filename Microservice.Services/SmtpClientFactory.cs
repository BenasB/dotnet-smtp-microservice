using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace Microservice.Services;

public class SmtpClientFactory : ISmtpClientFactory
{
    private readonly SmtpOptions _options;

    public SmtpClientFactory(IOptions<SmtpOptions> options)
    {
        _options = options.Value;
    }

    public async Task<ISmtpClient> CreateAsync()
    {
        var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_options.Host, _options.Port);

        return smtpClient;
    }
}
