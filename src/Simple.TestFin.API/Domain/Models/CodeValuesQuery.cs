namespace Simple.TestFin.API.Domain.Models;

public class CodeValuesQuery
{
    public int Page { get; set; } = 1;
    
    public int? PageSize { get; set; }

    //Filtering params
    public int? Code { get; set; }

    public string? ValueContains { get; set; }
}