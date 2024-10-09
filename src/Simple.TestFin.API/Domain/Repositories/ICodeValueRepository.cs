using System.Collections;
using Simple.TestFin.API.Domain.Entities;
using Simple.TestFin.API.Domain.Models;

namespace Simple.TestFin.API.Domain.Repositories;

public interface ICodeValueRepository
{
    Task<List<CodeValue>> GetList(CodeValuesQuery query, CancellationToken cancellationToken);

    Task ClearAndInsertItems(IEnumerable<CodeValue> items, CancellationToken cancellationToken);
}