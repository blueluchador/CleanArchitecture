using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Infrastructure.DataSourceConnectors;
using CleanArchitecture.Infrastructure.ORM;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Repositories;

public static class RepositoryExtensions
{
    public static IServiceCollection AddObjectMapper(this IServiceCollection services)
    {
        return services.AddSingleton<IObjectMapper, ObjectMapper>();
    }
    
    public static IServiceCollection AddHelloWorldRepository(this IServiceCollection services, string connectionString)
    {
        return services.AddSingleton<IDbConnectionFactory>(_ => new HelloWorldConnectionFactory(connectionString))
            .AddScoped<IHelloWorldRepository, HelloWorldRepository>();
    }
}