using Simple.TestFin.API.Domain.Entities;

namespace Simple.TestFin.API.Application.Services.Interfaces;

public interface IRequestLoggingService
{
    Task<List<RequestLog>> GetRequestLogs(CancellationToken cancellationToken);
    Task<RequestLog> LogRequest(HttpContext context, CancellationToken cancellationToken);
    // Task<ResponseLog> LogResponse(HttpContext response, CancellationToken cancellationToken);
}