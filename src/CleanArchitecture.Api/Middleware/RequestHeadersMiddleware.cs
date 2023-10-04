using CleanArchitecture.Application.Contracts.ContextItems;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Api.Middleware;

public class RequestHeadersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RequestHeaderOptions _options;

    public RequestHeadersMiddleware(RequestDelegate next, IOptions<RequestHeaderOptions> options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _next = next ?? throw new ArgumentNullException(nameof(next));
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context, IWriteableContextItems contextItems,
        ILogger<RequestHeadersMiddleware> logger)
    {
        foreach (string headerKey in _options.Headers)
        {
            if (!context.Request.Headers.TryGetValue(headerKey, out var value)) continue;
            logger.LogInformation("Setting context item '{Header}' to value='{Value}'", headerKey, value);
            contextItems.Set(headerKey, value);
        }

        await _next(context);
    }
}