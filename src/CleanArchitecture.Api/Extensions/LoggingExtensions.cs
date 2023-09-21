using Serilog;
using Serilog.Events;

namespace CleanArchitecture.Api.Extensions;

public static class LoggingExtensions
{
    public static IHostBuilder ConfigureLogging(this IHostBuilder host)
    {
        return host.UseSerilog((context, config) =>
            config.ReadFrom.Configuration(context.Configuration));
    }
    
    public static IApplicationBuilder UseCustomRequestLogging(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging(opts =>
        {
            opts.EnrichDiagnosticContext = EnrichFromRequest;
            opts.GetLevel = CustomGetLevel;
        });

        return app;
    }

    private static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        var request = httpContext.Request;

        // Set all the common properties available for every request
        diagnosticContext.Set("Host", request.Host);
        diagnosticContext.Set("Protocol", request.Protocol);
        diagnosticContext.Set("Scheme", request.Scheme);
        
        // Add appropriate request header information here.

        // Only set it if available. You're not sending sensitive data in a querystring right?
        if (request.QueryString.HasValue)
        {
            diagnosticContext.Set("QueryString", request.QueryString.Value);
        }

        // Set the content-type of the Response at this point
        diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

        // Retrieve the IEndpointFeature selected for the request
        var endpoint = httpContext.GetEndpoint();
        if (endpoint != null)
        {
            diagnosticContext.Set("EndpointName", endpoint.DisplayName);
        }

        diagnosticContext.Set("CorrelationId", httpContext.TraceIdentifier);
    }

    private static LogEventLevel CustomGetLevel(HttpContext context, double _, Exception? e)
    {
        // If there is an exception, automatically set it to error
        // Else if status code is 400-499, set as a warning
        // Else if status code is >=500, set as an error, everything else that isn't a health check is info.
        if (e == null)
        {
            return context.Response.StatusCode switch
            {
                >= 400 and < 500 => LogEventLevel.Warning,
                >= 500 => LogEventLevel.Error,
                _ => context.IsHealthCheckEndpoint() ? LogEventLevel.Verbose : LogEventLevel.Information
            };
        }

        return LogEventLevel.Error;
    }

    private static bool IsHealthCheckEndpoint(this HttpContext context)
    {
        // Set request logs for health check specific endpoints to verbose (lowest level) so they do not show up.
        // This is to prevent health check logs from cluttering up sumo. If there are any errors those will still get logged.
        var path = context.Request.Path;
        return path.Value != null && path.HasValue &&
               (path.Value.Contains("ping") || path.Value.Contains("healthcheck"));
    }
}