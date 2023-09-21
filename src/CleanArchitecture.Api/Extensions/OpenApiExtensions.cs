using System.Reflection;
using Microsoft.OpenApi.Models;

namespace CleanArchitecture.Api.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddApiDocs(this IServiceCollection services)
    {
        return services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Clean Architecture API",
                Version = "v1",
                Description = "The web service for Clean Architecture."
            });

            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });
    }

    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        if ((app as WebApplication)!.Environment.IsDevelopment())
        {
            app.UseSwagger()
                .UseReDoc(opt =>
                {
                    opt.DocumentTitle = "Clean Architecture API";
                    opt.SpecUrl = "/swagger/v1/swagger.json";
                });
        }

        return app;
    }
}