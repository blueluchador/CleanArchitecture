using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Application.Contracts.Services;
using CleanArchitecture.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Services;

public class HelloWorldService : IHelloWorldService
{
    private readonly IHelloWorldRepository _helloWorldRepository;
    private readonly ILogger<HelloWorldService> _logger;

    public HelloWorldService(IHelloWorldRepository helloWorldRepository, ILogger<HelloWorldService> logger)
    {
        _helloWorldRepository = helloWorldRepository;
        _logger = logger;
    }

    public async Task<HelloWorldMessage> GetMessage(Guid helloWorldId)
    {
        _logger.LogInformation("Get hello world message, HelloWorldId: {HelloWorldId}", helloWorldId);
        var helloWorld = await _helloWorldRepository.GetHelloWorld(helloWorldId);
        return helloWorld == null
            ? new HelloWorldMessage { Message = "Hello, World!" }
            : new HelloWorldMessage { Message = $"Hello, {helloWorld.FirstName}!" };
    }
}