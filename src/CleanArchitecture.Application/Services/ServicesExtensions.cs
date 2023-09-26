using CleanArchitecture.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Services;

public static class ServicesExtensions
{
    public static IServiceCollection AddApplicatopmServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IHelloWorldService, HelloWorldService>();
    }
}