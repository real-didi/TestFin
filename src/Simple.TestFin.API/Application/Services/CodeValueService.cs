using FluentValidation;
using Simple.TestFin.API.Application.Services.Interfaces;
using Simple.TestFin.API.Domain.Entities;
using Simple.TestFin.API.Domain.Models;
using Simple.TestFin.API.Domain.Repositories;
using Simple.TestFin.API.Infrastructure.Database.SqlServer;

namespace Simple.TestFin.API.Application.Services;

public class CodeValueService(TestFinDbContext context, ICodeValueRepository repository, 
    IValidator<IEnumerable<CodeValue>> validator) : ICodeValueService
{
    private readonly ICodeValueRepository _repository = repository;
    private readonly IValidator<IEnumerable<CodeValue>> _validator = validator;

    public async Task<List<CodeValue>> GetList(CodeValuesQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetList(query, cancellationToken);
    }

    public async Task SaveList(IEnumerable<CodeValue> values, CancellationToken cancellationToken)
    {
        var codeValues = values.OrderBy(q => q.Code).ToList();
        
        await _validator.ValidateAndThrowAsync(codeValues, cancellationToken);
        
        // Assign indexes, items were already sorted above
        int index = 0;
        foreach (var codeValue in codeValues)
        {
            codeValue.Index = ++index;
        }
        
        await _repository.ClearAndInsertItems(codeValues, cancellationToken);
    }
}