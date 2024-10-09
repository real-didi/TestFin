using Microsoft.AspNetCore.Mvc;
using Simple.TestFin.API.Application.Services.Interfaces;
using Simple.TestFin.API.Domain.Entities;

namespace Simple.TestFin.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LogsController(ILogger<LogsController> logger, IRequestLoggingService service) : ControllerBase
{
    private readonly ILogger<LogsController> _logger = logger;
    private readonly IRequestLoggingService _service = service;

    [HttpGet("requests")]
    public async Task<IEnumerable<RequestLog>> GetRequestLogs(CancellationToken cancellationToken)
    {
        return await _service.GetRequestLogs(cancellationToken);
    }
}