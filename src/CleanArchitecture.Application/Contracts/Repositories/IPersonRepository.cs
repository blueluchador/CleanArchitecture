using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Contracts.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetPersons();
    
    Task<Person?> GetPersonById(Guid personId);
}