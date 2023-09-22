using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Contracts.Repositories;

public interface IHelloWorldRepository
{
    Task<HelloWorld> GetHelloWorld(Guid helloWorldId);
}