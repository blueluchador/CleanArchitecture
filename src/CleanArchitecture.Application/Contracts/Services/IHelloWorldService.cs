using CleanArchitecture.Application.DTOs;

namespace CleanArchitecture.Application.Contracts.Services;

public interface IHelloWorldService
{
    Task<HelloWorldMessage> GetMessage(Guid helloWorldId);
}