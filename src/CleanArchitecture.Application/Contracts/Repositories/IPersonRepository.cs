using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Contracts.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetPersons(Guid tenantId);
    
    Task<Person?> GetPersonById(Guid personId);

    Task<Guid?> AddPerson(Person person);
}