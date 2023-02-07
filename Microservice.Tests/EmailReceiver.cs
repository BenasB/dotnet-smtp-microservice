using MimeKit;
using System.Net.Http.Json;

namespace Microservice.Tests;

/// <summary>
/// Queries MailHog's API to retrieve received emails. Primary use is to assert sent emails.
/// </summary>
internal class EmailReceiver
{
    private readonly HttpClient _client;

	public EmailReceiver(ApiOptions options)
	{
        _client = new HttpClient()
        {
            BaseAddress = new UriBuilder()
            {
                Host = options.Host,
                Port = options.Port,
                Path = "/api/v2/"
            }.Uri,
        };
    }

    public async Task<IEnumerable<Email>> GetAllEmailsAsync()
    {
        var httpResponse = await _client.GetAsync("messages");

        if (!httpResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to query MailHog's API. Status code: {httpResponse.StatusCode}");

        var allMessagesResponse = await httpResponse.Content.ReadFromJsonAsync<AllMessagesResponse>();

        return allMessagesResponse?.Items ?? Enumerable.Empty<Email>();
    }

    public record ApiOptions
    {
        public string Host { get; init; } = null!;
        public int Port { get; init; } = 80;
    }

    public record Email
    {
        public EmailAddress From { get; init; } = new();
        public IEnumerable<EmailAddress> To { get; init; } = Enumerable.Empty<EmailAddress>();
        public EmailContent Content { get; set; } = new();
    }

    public record EmailAddress
    {
        public string Mailbox { get; init; } = null!;
        public string Domain { get; init; } = null!;
    }

    public record EmailContent
    {
        public EmailHeaders Headers { get; set; } = new();
        public string Body { get; init; } = null!;
    }

    public record EmailHeaders
    {
        public IEnumerable<string> Subject { get; init; } = Enumerable.Empty<string>();
    }

    private record AllMessagesResponse
    {
        public int Total { get; init; }
        public IEnumerable<Email> Items { get; init; } = Enumerable.Empty<Email>();
    }
}
