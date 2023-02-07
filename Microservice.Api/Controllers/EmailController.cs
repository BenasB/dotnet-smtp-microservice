using Microsoft.AspNetCore.Mvc;

namespace Microservice.Api.Controllers;

[ApiController]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;

    public EmailController(ILogger<EmailController> logger)
    {
        _logger = logger;
    }

    [HttpPost("/send")]
    public IActionResult Send()
    {
        return Ok();
    }
}