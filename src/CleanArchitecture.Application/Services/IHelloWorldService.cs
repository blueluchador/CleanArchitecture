using CleanArchitecture.Application.DTOs;

namespace CleanArchitecture.Application.Services;

public interface IHelloWorldService
{
    Task<HelloWorldMessage> GetMessage(Guid personId);
}