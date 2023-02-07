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
    private readonly MailHogFixture _fixture;

    public EmailClientTests(MailHogFixture fixture)
    {
        _fixture = fixture;
        _smtpOptions = Options.Create<SmtpOptions>(new()
        {
            Host = "localhost",
            Port = _fixture.Container.GetMappedPublicPort(MailHogFixture.SmtpContainerPort)
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
            Subject = "Testing is cool!",
            Body = "This is sent from a test"
        };

        await emailClient.SendAsync(request);
    }
}