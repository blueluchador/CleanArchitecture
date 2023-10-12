using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.TestFixtures;
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
        var person = Fake.Create<Person>();
        var mock = Mock.Get(_personRepository);
        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync(person);
        
        // Act
        var helloWorldService = new HelloWorldService(_personRepository, _logger);
        var result = await helloWorldService.GetMessage(person.Uuid);
        
        // Assert
        mock.Verify(m => m.GetPersonById(person.Uuid), Times.Once);

        result.Message.Should()
            .Be($"Hello, {person.FirstName}!", $"because the repository returns '{person.FirstName}'");
    }
    
    [Fact]
    public async Task GetHelloWorldMessage_ReturnsDefaultMessage()
    {
        // Arrange
        var mock = Mock.Get(_personRepository);
        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync((Person?)null);
        
        // Act
        var helloWorldService = new HelloWorldService(_personRepository, _logger);
        var result = await helloWorldService.GetMessage(Guid.NewGuid());
        
        // Assert
        mock.Verify(m => m.GetPersonById(It.IsAny<Guid>()), Times.Once);

        result.Message.Should().Be("Hello, World!", "because the repository returns null");
    }
}