using Simple.TestFin.API.Application.Services.Interfaces;

namespace Simple.TestFin.API.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IRequestLoggingService logService)
    {
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        await logService.LogRequest(context, context.RequestAborted);
        await responseBody.CopyToAsync(originalBodyStream, context.RequestAborted);
    }
}