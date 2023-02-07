using Microservice.Interface;
using Microservice.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Microservice.Tests;

public class EmailClientTests : IClassFixture<MailHogFixture>
{
    private static readonly ILogger<EmailClient> Logger = new Mock<ILogger<EmailClient>>().Object;
    private readonly IOptions<SmtpOptions> _smtpOptions;
    private readonly EmailReceiver _emailReceiver;
    private readonly MailHogFixture _fixture;

    public EmailClientTests(MailHogFixture fixture)
    {
        _fixture = fixture;

        _smtpOptions = Options.Create<SmtpOptions>(new()
        {
            Host = _fixture.Container.Hostname,
            Port = _fixture.Container.GetMappedPublicPort(MailHogFixture.SmtpContainerPort)
        });

        _emailReceiver = new EmailReceiver(new EmailReceiver.ApiOptions()
        {
            Host = _fixture.Container.Hostname,
            Port = _fixture.Container.GetMappedPublicPort(MailHogFixture.WebContainerPort)
        });
    }

    [Fact]
    public async Task SendsEmail()
    {
        ISmtpClientFactory smtpClientFactory = new SmtpClientFactory(_smtpOptions);
        IEmailClient emailClient = new EmailClient(Logger, smtpClientFactory);

        var request = new SendEmailRequest()
        {
            From = "usera@testing.com",
            To = "userb@testing.com",
            Subject = "Testing is cool! " + Guid.NewGuid(),
            Body = "This is sent from a test"
        };

        await emailClient.SendAsync(request);

        var allEmails = await _emailReceiver.GetAllEmailsAsync();

        // Find the email we just sent by unique subject
        var email = Assert.Single(allEmails.Where(e => e.Content.Headers.Subject.SingleOrDefault() == request.Subject));

        Assert.Equal(request.From, $"{email.From.Mailbox}@{email.From.Domain}");

        var recipient = Assert.Single(email.To);
        Assert.Equal(request.To, $"{recipient.Mailbox}@{recipient.Domain}");

        Assert.Equal(request.Body, email.Content.Body);
    }
}