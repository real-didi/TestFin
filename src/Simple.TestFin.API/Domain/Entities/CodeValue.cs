namespace Simple.TestFin.API.Domain.Entities;

public class CodeValue
{
    public int Id { get; set; }
    
    public int Index { get; set; }
    
    public int Code { get; set; }
    
    public required string Value { get; set; }
}