using CleanArchitecture.Api.Middleware;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>();
    }
    
    public static IApplicationBuilder UsePingEndpointMiddleware(this IApplicationBuilder app)
    {
        return app.MapWhen(
            context => context.Request.Method == HttpMethods.Get &&
                       context.Request.Path.StartsWithSegments("/ping", out var remaining) &&
                       String.IsNullOrEmpty(remaining),
            mapApp => { mapApp.Run(async context => await context.Response.WriteAsync("pong")); });
    }
    
    public static IApplicationBuilder UseRequestHeadersMiddleware(this IApplicationBuilder app,
        IEnumerable<string> headers)
    {
        return app.UseRequestHeadersMiddleware(new RequestHeaderOptions { Headers = headers });
    }

    public static IApplicationBuilder UseRequestHeadersMiddleware(this IApplicationBuilder app,
        RequestHeaderOptions options)
    {
        return app.UseMiddleware<RequestHeadersMiddleware>(Options.Create(options));
    }
}