using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;

namespace CleanArchitecture.Api.Extensions;

public static class ProblemDetailsExtensions
{
    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
    {
        return services.AddProblemDetails(opt =>
        {
            opt.Rethrow<NotSupportedException>();
            // TODO: What project will this exception come from?
            //opt.MapToStatusCode<ItemNotFoundException>(StatusCodes.Status404NotFound);
            opt.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
            opt.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
            opt.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);

            opt.IncludeExceptionDetails = (context, _) =>
            {
                var env = context.RequestServices.GetRequiredService<IHostEnvironment>();
                return env.IsDevelopment() || env.IsStaging();
            };

            opt.OnBeforeWriteDetails = (context, _) =>
            {
                var logger = context.RequestServices.GetService<ILogger<Program>>();
                if (logger == null)
                    return;
                
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature == null)
                {
                    logger.LogError("An unexpected error occured for the request {Url}",
                        context.Request.GetDisplayUrl());
                    return;
                }

                var exception = contextFeature.Error;
                logger.LogError(exception, "The request at {Url} threw an error with the message: {Message}",
                    context.Request.GetDisplayUrl(), exception.Message);
            };
        });
    }
}