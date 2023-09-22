using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Repositories;

public class HelloWorldRepository : IHelloWorldRepository
{
    public Task<HelloWorld> GetHelloWorld(Guid helloWorldId)
    {
        throw new NotImplementedException();
    }
}