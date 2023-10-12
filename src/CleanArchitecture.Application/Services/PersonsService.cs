using CleanArchitecture.Application.Contracts.ContextItems;
using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Domain.Constants;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Services;

public class PersonsService : IPersonsService
{
    private readonly IPersonRepository _personRepository;
    private readonly IContextItems _contextItems;
    private readonly ILogger<PersonsService> _logger;

    public PersonsService(IPersonRepository personRepository, IContextItems contextItems,
        ILogger<PersonsService> logger)
    {
        _personRepository = personRepository;
        _contextItems = contextItems;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetPersons()
    {
        var tenantId = Guid.Parse(_contextItems.Get(ApiHeaders.TenantId));
        _logger.LogInformation("Get all Persons from Person repository for tenant '{Tenant}'", tenantId);

        var persons = await _personRepository.GetPersons(tenantId);

        return persons.Select(p => new Person
        {
            PersonId = p.Uuid,
            FirstName = p.FirstName,
            LastName = p.LastName
        });
    }

    public async Task<Person?> GetPersonById(Guid personId)
    {
        _logger.LogInformation("Get Person '{PersonId}' from Person repository.", personId);
        
        var person = await _personRepository.GetPersonById(personId);

        if (person == null)
        {
            return null;
        }
        
        return new Person
        {
            PersonId = person.Uuid,
            FirstName = person.FirstName,
            LastName = person.LastName
        };
    }

    public async Task<Guid?> AddPerson(Person person)
    {
        var tenantId = Guid.Parse(_contextItems.Get(ApiHeaders.TenantId));
        _logger.LogInformation("Add Person '{Person}' to repository for tenant '{Tenant}'", person, tenantId);

        return await _personRepository.AddPerson(new PersonEntity
        {
            FirstName = person.FirstName,
            LastName = person.LastName
        }, tenantId);
    }
}