using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence.EmbeddedSqlResources;
using CleanArchitecture.Infrastructure.Persistence.ORM;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IObjectMapper _objectMapper;
    private readonly ILogger<PersonRepository> _logger;

    public PersonRepository(IObjectMapper objectMapper, ILogger<PersonRepository> logger)
    {
        _objectMapper = objectMapper;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetPersons(Guid tenantId)
    {
        _logger.LogInformation("Get Persons from Hello World database. Tenant ID: '{TenantID}'", tenantId);

        var @params = new { tenantUuid = tenantId };

        return await _objectMapper.QueryAsync<Person>(Resource.GetPersonsSqlQuery, @params);
    }

    public async Task<Person?> GetPersonById(Guid personId)
    {
        _logger.LogInformation("Get Person '{PersonId}' from Hello World database", personId);
        
        var @params = new { uuid = personId };

        return await _objectMapper.QuerySingleOrDefaultAsync<Person>(Resource.GetPersonByIdSqlQuery, @params);
    }

    public async Task<Guid?> AddPerson(Person person, Guid tenantId)
    {
        _logger.LogInformation("Add Person '{Person}' to Hello World database", person);

        var @params = new { firstName = person.FirstName, lastName = person.LastName, tenantId };

        try
        {
            return (await _objectMapper.QuerySingleAsync<Person>(Resource.AddPersonSqlQuery, @params)).Uuid;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured adding '{Person}' to Hello World database", person);
            return null;
        }
    }
}