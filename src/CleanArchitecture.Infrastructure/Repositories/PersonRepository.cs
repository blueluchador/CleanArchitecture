using CleanArchitecture.Application.Contracts.ContextItems;
using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.EmbeddedSqlResources;
using CleanArchitecture.Infrastructure.ORM;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IObjectMapper _objectMapper;
    private readonly IContextItems _contextItems;
    private readonly ILogger<PersonRepository> _logger;

    public PersonRepository(IObjectMapper objectMapper, IContextItems contextItems, ILogger<PersonRepository> logger)
    {
        _objectMapper = objectMapper;
        _contextItems = contextItems;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetPersons()
    {
        var tenantUuid = Guid.Parse(_contextItems.Get(ApiHeaders.TenantId));
        _logger.LogInformation("Get Persons from Hello World Database. Tenant ID: '{TenantID}'", tenantUuid);

        var @params = new { tenantUuid };

        return await _objectMapper.QueryAsync<Person>(Resource.GetPersonsSqlQuery, @params);
    }

    public async Task<Person?> GetPersonById(Guid personId)
    {
        _logger.LogInformation("Get Person '{PersonId}' from Hello World Database", personId);
        
        var @params = new { uuid = personId };

        return await _objectMapper.QuerySingleOrDefaultAsync<Person?>(Resource.GetPersonByIdSqlQuery, @params);
    }
}