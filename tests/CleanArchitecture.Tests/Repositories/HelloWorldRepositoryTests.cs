using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.ORM;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Tests.Repositories;

public class HelloWorldRepositoryTests
{
    private readonly IObjectMapper _objectMapper = Mock.Of<IObjectMapper>();
    private readonly ILogger<HelloWorldRepository> _logger = Mock.Of<ILogger<HelloWorldRepository>>();

    [Fact]
    public async Task GetHelloWorld_ReturnsSingleRow()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);

        mock.Setup(m =>
                m.QuerySingleOrDefaultAsync<Person?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(new Person());

        // Act
        var repository = new HelloWorldRepository(_objectMapper, _logger);
        var result = await repository.GetHelloWorld(Guid.NewGuid());

        // Assert
        mock.Verify(
            m => m.QuerySingleOrDefaultAsync<Person?>(It.IsNotNull<string>(), It.IsNotNull<object>(), null, null,
                null), Times.Once);
        
        result.Should().NotBeNull("because the hello world row exists");
    }
    
    [Fact]
    public async Task GetHelloWorld_ReturnsNull()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);

        Person? helloWorld = null;
        mock.Setup(m =>
                m.QuerySingleOrDefaultAsync<Person?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(helloWorld);

        // Act
        var repository = new HelloWorldRepository(_objectMapper, _logger);
        var result = await repository.GetHelloWorld(Guid.NewGuid());

        // Assert
        mock.Verify(
            m => m.QuerySingleOrDefaultAsync<Person?>(It.IsNotNull<string>(), It.IsNotNull<object>(), null, null,
                null), Times.Once);
        
        result.Should().BeNull("because the hello world row does not exist");
    }
}