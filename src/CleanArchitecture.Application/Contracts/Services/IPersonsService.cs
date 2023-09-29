using CleanArchitecture.Application.DTOs;

namespace CleanArchitecture.Application.Contracts.Services;

public interface IPersonsService
{
    Task<IEnumerable<Person>> GetPersons();
    
    Task<Person?> GetPersonById(Guid personId);
}