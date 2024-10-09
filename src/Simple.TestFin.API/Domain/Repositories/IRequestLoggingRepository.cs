using Simple.TestFin.API.Domain.Entities;

namespace Simple.TestFin.API.Domain.Repositories;

public interface IRequestLoggingRepository
{
    Task<RequestLog> CreateRequestLog(RequestLog log, CancellationToken cancellationToken);
    Task<List<RequestLog>> GetRequestLogs(CancellationToken cancellationToken);
}