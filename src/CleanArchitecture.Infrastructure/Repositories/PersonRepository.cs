using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.EmbeddedSqlResources;
using CleanArchitecture.Infrastructure.ORM;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IObjectMapper _objectMapper;
    private readonly ILogger<PersonRepository> _logger;

    public PersonRepository(IObjectMapper objectMapper, ILogger<PersonRepository> logger)
    {
        _objectMapper = objectMapper;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetPersons()
    {
        _logger.LogInformation("Get Persons from Hello World Database.");

        return await _objectMapper.QueryAsync<Person>(Resource.GetPersonsSqlQuery);
    }

    public async Task<Person?> GetPersonById(Guid personId)
    {
        _logger.LogInformation("Get Person '{PersonId}' from Hello World Database", personId);
        
        var @params = new { uuid = personId };

        return await _objectMapper.QuerySingleOrDefaultAsync<Person?>(Resource.GetPersonByIdSqlQuery, @params);
    }
}