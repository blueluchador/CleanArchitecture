using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Application.Contracts.Services;
using CleanArchitecture.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Services;

public class PersonsService : IPersonsService
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<PersonsService> _logger;

    public PersonsService(IPersonRepository personRepository, ILogger<PersonsService> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetPersons()
    {
        _logger.LogInformation("Get all Persons from Person repository.");

        var persons = await _personRepository.GetPersons();

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
}