using Microsoft.AspNetCore.Mvc;
using Simple.TestFin.API.Application.Services.Interfaces;
using Simple.TestFin.API.Domain.Entities;
using Simple.TestFin.API.Domain.Models;

namespace Simple.TestFin.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CodeValueController(ILogger<CodeValueController> logger, ICodeValueService service) : ControllerBase
{
    private readonly ILogger<CodeValueController> _logger = logger;
    private readonly ICodeValueService _service = service;

    [HttpGet]
    public async Task<IEnumerable<CodeValue>> Get([FromQuery] CodeValuesQuery query, CancellationToken cancellationToken)
    {
        return await _service.GetList(query, cancellationToken);
    }

    [HttpPost]
    public async Task<IActionResult> Post(IEnumerable<CodeValueRequest> values, CancellationToken cancellationToken)
    {
        var items = new List<CodeValue>();
        
        int code = -1;
        foreach (var item in values)
        {
            // If we can't parse row code - fail the whole request
            if (!Int32.TryParse(item.Code, out code))
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, 
                    $"Unable to parse code property '{item.Code}'.");
            }
            
            items.Add(new CodeValue
            {
                Code = code,
                Value = item.Value
            });   
        }
        
        await _service.SaveList(items, cancellationToken);
        
        return Ok();
    }
}