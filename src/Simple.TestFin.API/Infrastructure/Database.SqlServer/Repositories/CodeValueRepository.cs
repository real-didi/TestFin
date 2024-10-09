using System.Data;
using Microsoft.EntityFrameworkCore;
using Simple.TestFin.API.Domain.Entities;
using Simple.TestFin.API.Domain.Models;
using Simple.TestFin.API.Domain.Repositories;

namespace Simple.TestFin.API.Infrastructure.Database.SqlServer.Repositories;

public class CodeValueRepository(TestFinDbContext context) : ICodeValueRepository
{
    private readonly TestFinDbContext _context = context;
    
    public async Task<List<CodeValue>> GetList(CodeValuesQuery requestQuery, CancellationToken cancellationToken)
    {
        var query = _context.CodeValues.AsQueryable();
        if (requestQuery.Code.HasValue)
        {
            query = query.Where(q => q.Code == requestQuery.Code.Value);
        }

        if (!string.IsNullOrEmpty(requestQuery.ValueContains))
        {
            query = query.Where(q => q.Value.Contains(requestQuery.ValueContains));
        }

        if (requestQuery.PageSize.HasValue)
        {
            query = query
                .Skip((requestQuery.Page - 1) * requestQuery.PageSize.Value)
                .Take(requestQuery.PageSize.Value);
        }
        
        return await query.ToListAsync(cancellationToken);
    }

    public async Task ClearAndInsertItems(IEnumerable<CodeValue> items, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(
            IsolationLevel.Snapshot, cancellationToken);

        try
        {
            // Truncate current CodeValue table
            await context.CodeValues.ExecuteDeleteAsync(cancellationToken: cancellationToken);
        
            // Insert new CodeValue rows
            await _context.CodeValues.AddRangeAsync(items, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}