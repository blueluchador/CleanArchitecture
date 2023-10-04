using CleanArchitecture.Application.Contracts.ContextItems;
using CleanArchitecture.Application.Exceptions;
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
        foreach (var header in _options.Headers)
        {
            if (!context.Request.Headers.TryGetValue(header.Key, out var value))
            {
                if (!header.IsRequired) continue;
                throw new BadRequestException($"The required header '{header.Key}' is missing from the request.");
            }
            logger.LogInformation("Setting context item '{Header}' to value='{Value}'", header.Key, value);
            contextItems.Set(header.Key, value);
        }

        await _next(context);
    }
}