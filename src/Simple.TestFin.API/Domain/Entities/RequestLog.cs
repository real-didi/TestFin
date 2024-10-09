namespace Simple.TestFin.API.Domain.Entities;

public class RequestLog
{
    public long Id { get; set; }
    
    public required string Method { get; set; }
    
    public required string Path { get; set; }
    
    public required string Headers { get; set; }

    public required string Body { get; set; }
    
    public int StatusCode { get; set; }

    public required string ResponseBody { get; set; }

    public DateTime Timestamp { get; set; }
}