using CleanArchitecture.Application.Contracts.Services;
using CleanArchitecture.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Tests.Services;

public static class ServicesCollection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IHelloWorldService, HelloWorldService>();
    }
}