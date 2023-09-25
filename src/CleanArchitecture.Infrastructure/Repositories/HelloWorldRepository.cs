using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.EmbeddedSqlResources;
using CleanArchitecture.Infrastructure.ORM;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Repositories;

public class HelloWorldRepository : IHelloWorldRepository
{
    private readonly IObjectMapper _objectMapper;
    private readonly ILogger<HelloWorldRepository> _logger;

    public HelloWorldRepository(IObjectMapper objectMapper, ILogger<HelloWorldRepository> logger)
    {
        _objectMapper = objectMapper;
        _logger = logger;
    }

    public async Task<Person?> GetHelloWorld(Guid helloWorldId)
    {
        _logger.LogInformation("Get hello world, HelloWorldID: {HelloWorldID}", helloWorldId);
        
        var @params = new { uuid = helloWorldId };

        return await _objectMapper.QuerySingleOrDefaultAsync<Person?>(Resource.GetPersonByIdSqlQuery, @params);
    }
}