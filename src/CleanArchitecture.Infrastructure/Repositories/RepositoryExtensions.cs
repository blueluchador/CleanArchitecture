using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Infrastructure.DataSourceConnectors;
using CleanArchitecture.Infrastructure.ORM;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Repositories;

public static class RepositoryExtensions
{
    public static IServiceCollection AddHelloWorldRepository(this IServiceCollection services, string connectionString)
    {
        return services
            .AddSingleton<IObjectMapper>(_ => new HelloWorldObjectMapper(new NpgsqlConnectionFactory(connectionString)))
            .AddScoped<IHelloWorldRepository, HelloWorldRepository>();
    }
}