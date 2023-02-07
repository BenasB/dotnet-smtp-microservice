using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Microservice.Tests;

public class MailHogFixture : IAsyncLifetime
{
    public const int SmtpContainerPort = 1025;
    public const int WebContainerPort = 8025;
    private const int SmtpHostPort = 9000;
    private const int WebHostPort = 9001;

    private IContainer? _container;
    public IContainer Container => _container ?? throw new InvalidOperationException("Container not initialized");

    public Task DisposeAsync() => _container?.DisposeAsync().AsTask() ?? Task.CompletedTask;

    public Task InitializeAsync()
    {
        _container = new ContainerBuilder()
            .WithImage("mailhog/mailhog")
            .WithPortBinding(SmtpHostPort, SmtpContainerPort)
            .WithPortBinding(WebHostPort, WebContainerPort)
            .WithWaitStrategy(
                Wait.ForUnixContainer()
                .UntilPortIsAvailable(SmtpContainerPort)
                .UntilHttpRequestIsSucceeded(request => request.ForPort(WebContainerPort).ForPath("/"))
            )
            .Build();

        return _container.StartAsync();
    }
}
