using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Services;

public class HelloWorldService : IHelloWorldService
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<HelloWorldService> _logger;

    public HelloWorldService(IPersonRepository personRepository, ILogger<HelloWorldService> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    public async Task<HelloWorldMessage> GetMessage(Guid personId)
    {
        _logger.LogInformation("Get Person '{PersonId}' from Person repository.", personId);
        
        var person = await _personRepository.GetPersonById(personId);
        return person == null
            ? new HelloWorldMessage { Message = "Hello, World!" }
            : new HelloWorldMessage { Message = $"Hello, {person.FirstName}!" };
    }
}