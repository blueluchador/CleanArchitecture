using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Tests.Services;

public class HelloWorldServicesTests
{
    private readonly IHelloWorldRepository _helloWorldRepository = Mock.Of<IHelloWorldRepository>();
    private readonly ILogger<HelloWorldService> _logger = Mock.Of<ILogger<HelloWorldService>>();

    [Fact]
    public async Task GetHelloWorldMessage_ReturnsMessage()
    {
        // Arrange
        var mock = Mock.Get(_helloWorldRepository);
        mock.Setup(m => m.GetHelloWorld(It.IsAny<Guid>())).ReturnsAsync(new Person { FirstName = "Frankie" });
        
        // Act
        var helloWorldService = new HelloWorldService(_helloWorldRepository, _logger);
        var result = await helloWorldService.GetMessage(Guid.NewGuid());
        
        // Assert
        mock.Verify(m => m.GetHelloWorld(It.IsAny<Guid>()), Times.Once);

        result.Message.Should().Be("Hello, Frankie!", "because the repository returns 'Frankie'");
    }
    
    [Fact]
    public async Task GetHelloWorldMessage_ReturnsDefaultMessage()
    {
        // Arrange
        var mock = Mock.Get(_helloWorldRepository);
        Person? helloWorld = null;
        mock.Setup(m => m.GetHelloWorld(It.IsAny<Guid>())).ReturnsAsync(helloWorld);
        
        // Act
        var helloWorldService = new HelloWorldService(_helloWorldRepository, _logger);
        var result = await helloWorldService.GetMessage(Guid.NewGuid());
        
        // Assert
        mock.Verify(m => m.GetHelloWorld(It.IsAny<Guid>()), Times.Once);

        result.Message.Should().Be("Hello, World!", "because the repository returns null");
    }
}