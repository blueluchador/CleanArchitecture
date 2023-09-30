using Microsoft.Extensions.Primitives;

namespace CleanArchitecture.Api.Middleware;

public class CorrelationIdMiddleware
{
    private const string HeaderKey = "X-Correlation-ID";
    
    private readonly RequestDelegate _next;
    
    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        context.TraceIdentifier = context.Request.Headers.TryGetValue(HeaderKey, out var value)
            ? value.Single()
            : Guid.NewGuid().ToString();
        
        // apply the correlation ID to the response header for client side tracking
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Add(HeaderKey, new[] { context.TraceIdentifier });
            return Task.CompletedTask;
        });
        
       await _next(context);
    }
}