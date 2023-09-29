using CleanArchitecture.Application.Contracts.Services;
using CleanArchitecture.Application.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Services;

public static class ServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IHelloWorldService, HelloWorldService>()
            .AddScoped<IPersonsService, PersonsService>();
    }
}