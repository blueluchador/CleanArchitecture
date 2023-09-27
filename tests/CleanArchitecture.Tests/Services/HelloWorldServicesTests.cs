using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Tests.Services;

public class HelloWorldServicesTests
{
    private readonly IPersonRepository _personRepository = Mock.Of<IPersonRepository>();
    private readonly ILogger<HelloWorldService> _logger = Mock.Of<ILogger<HelloWorldService>>();

    [Fact]
    public async Task GetHelloWorldMessage_ReturnsMessage()
    {
        // Arrange
        var mock = Mock.Get(_personRepository);
        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync(new Person { FirstName = "Frankie" });
        
        // Act
        var helloWorldService = new HelloWorldService(_personRepository, _logger);
        var result = await helloWorldService.GetMessage(Guid.NewGuid());
        
        // Assert
        mock.Verify(m => m.GetPersonById(It.IsAny<Guid>()), Times.Once);

        result.Message.Should().Be("Hello, Frankie!", "because the repository returns 'Frankie'");
    }
    
    [Fact]
    public async Task GetHelloWorldMessage_ReturnsDefaultMessage()
    {
        // Arrange
        var mock = Mock.Get(_personRepository);
        Person? helloWorld = null;
        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync(helloWorld);
        
        // Act
        var helloWorldService = new HelloWorldService(_personRepository, _logger);
        var result = await helloWorldService.GetMessage(Guid.NewGuid());
        
        // Assert
        mock.Verify(m => m.GetPersonById(It.IsAny<Guid>()), Times.Once);

        result.Message.Should().Be("Hello, World!", "because the repository returns null");
    }
}