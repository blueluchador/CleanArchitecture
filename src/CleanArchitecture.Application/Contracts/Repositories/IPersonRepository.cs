using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Contracts.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetPersonById(Guid helloWorldId);
}