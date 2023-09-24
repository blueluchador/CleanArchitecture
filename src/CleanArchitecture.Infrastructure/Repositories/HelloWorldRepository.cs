using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.DataSourceConnectors;
using CleanArchitecture.Infrastructure.EmbeddedSqlResources;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Repositories;

public class HelloWorldRepository : IHelloWorldRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<HelloWorldRepository> _logger;

    public HelloWorldRepository(IDbConnectionFactory connectionFactory, ILogger<HelloWorldRepository> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task<HelloWorld?> GetHelloWorld(Guid helloWorldId)
    {
        _logger.LogInformation("Get hello world, Hello World ID: {HelloWorldID}", helloWorldId);
        
        var @params = new { uuid = helloWorldId };
        
        using var conn = await _connectionFactory.CreateConnectionAsync();
        return await conn.QuerySingleOrDefaultAsync<HelloWorld?>(Resource.GetHelloWorldQuery, @params);
    }
}