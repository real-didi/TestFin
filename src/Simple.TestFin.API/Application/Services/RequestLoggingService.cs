using System.Text.Json;
using Simple.TestFin.API.Application.Services.Interfaces;
using Simple.TestFin.API.Domain.Entities;
using Simple.TestFin.API.Domain.Repositories;

namespace Simple.TestFin.API.Application.Services;

public class RequestLoggingService : IRequestLoggingService
{
    private readonly IRequestLoggingRepository _repository;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public RequestLoggingService(IRequestLoggingRepository repository, IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<List<RequestLog>> GetRequestLogs(CancellationToken cancellationToken)
    {
        return await _repository.GetRequestLogs(cancellationToken);
    }
    
    public async Task<RequestLog> LogRequest(HttpContext context, CancellationToken cancellationToken)
    {
        var request = context.Request;

        // Enable buffering so request.Body.Seek is allowed in the ReadBody method
        context.Request.EnableBuffering();
        
        var log = new RequestLog
        {
            Method = request.Method,
            Path = request.Path,
            Headers = JsonSerializer.Serialize(request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())),
            Body = await ReadBody(request.Body),
            ResponseBody = await ReadBody(context.Response.Body),
            StatusCode = context.Response.StatusCode,
            Timestamp = _dateTimeProvider.UtcNow()
        };

        return await _repository.CreateRequestLog(log, cancellationToken);
    }
    
    private async Task<string> ReadBody(Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(stream).ReadToEndAsync();
        stream.Seek(0, SeekOrigin.Begin);

        return body;
    }
}