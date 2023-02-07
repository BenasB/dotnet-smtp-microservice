using Microservice.Interface;
using Microservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Api.Controllers;

[ApiController]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly IEmailClient _emailClient;

    public EmailController(ILogger<EmailController> logger, IEmailClient emailClient)
    {
        _logger = logger;
        _emailClient = emailClient;
    }

    [HttpPost("/send")]
    public async Task<IActionResult> Send(SendEmailRequest request)
    {
        _logger.LogInformation("Sending {@Request}", request);

        await _emailClient.SendAsync(request);

        return Ok();
    }
}