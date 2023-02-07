using Microservice.Interface;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace Microservice.Services;

public class EmailClient : IEmailClient
{
    private readonly ISmtpClientFactory _smtpClientFactory;
    private readonly ILogger<EmailClient> _logger;

    public EmailClient(ILogger<EmailClient> logger, ISmtpClientFactory smtpClientFactory)
    {
        _logger = logger;
        _smtpClientFactory = smtpClientFactory;
    }

    public async Task SendAsync(SendEmailRequest request)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(request.From));
        message.To.Add(MailboxAddress.Parse(request.To));
        message.Subject = request.Subject;

        message.Body = new TextPart(TextFormat.Text)
        {
            Text = request.Body
        };

        using var client = await _smtpClientFactory.CreateAsync();
        var result = await client.SendAsync(message);

        _logger.LogDebug("Finished sending email with {@result}", result);
    }
}
