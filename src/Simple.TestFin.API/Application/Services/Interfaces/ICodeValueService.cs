using Simple.TestFin.API.Domain.Entities;
using Simple.TestFin.API.Domain.Models;

namespace Simple.TestFin.API.Application.Services.Interfaces;

public interface ICodeValueService
{
    Task<List<CodeValue>> GetList(CodeValuesQuery query, CancellationToken cancellationToken);

    Task SaveList(IEnumerable<CodeValue> values, CancellationToken cancellationToken);
}