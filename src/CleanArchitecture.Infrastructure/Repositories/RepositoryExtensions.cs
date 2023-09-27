using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Infrastructure.DataSourceConnectors;
using CleanArchitecture.Infrastructure.ORM;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Repositories;

public static class RepositoryExtensions
{
    public static IServiceCollection AddHelloWorldRepository(this IServiceCollection services, string connectionString)
    {
        return services
            .AddScoped<IPersonRepository>(p => new PersonRepository(
                new HelloWorldObjectMapper(new NpgsqlConnectionFactory(connectionString)),
                p.GetRequiredService<ILogger<PersonRepository>>()));
    }
}