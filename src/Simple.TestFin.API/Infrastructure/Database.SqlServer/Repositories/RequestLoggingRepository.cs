using Microsoft.EntityFrameworkCore;
using Simple.TestFin.API.Domain.Entities;
using Simple.TestFin.API.Domain.Repositories;

namespace Simple.TestFin.API.Infrastructure.Database.SqlServer.Repositories;

public class RequestLoggingRepository : IRequestLoggingRepository
{
    private readonly TestFinDbContext _context;

    public RequestLoggingRepository(TestFinDbContext context)
    {
        _context = context;
    }
    
    public async Task<RequestLog> CreateRequestLog(RequestLog log, CancellationToken cancellationToken)
    {
        await _context.RequestLogs.AddAsync(log, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return log;
    }

    public async Task<List<RequestLog>> GetRequestLogs(CancellationToken cancellationToken)
    {
        return await _context.Set<RequestLog>().ToListAsync(cancellationToken: cancellationToken);
    }
}