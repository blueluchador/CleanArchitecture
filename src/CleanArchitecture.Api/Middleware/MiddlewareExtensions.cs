namespace CleanArchitecture.Api.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UsePingEndpointMiddleware(this IApplicationBuilder app)
    {
        return app.MapWhen(
            context => context.Request.Method == HttpMethods.Get &&
                       context.Request.Path.StartsWithSegments("/ping", out var remaining) &&
                       String.IsNullOrEmpty(remaining),
            mapApp => { mapApp.Run(async context => await context.Response.WriteAsync("pong")); });
    }
}