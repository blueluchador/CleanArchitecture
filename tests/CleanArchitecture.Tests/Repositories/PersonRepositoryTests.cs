using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.ORM;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Tests.Repositories;

public class PersonRepositoryTests
{
    private readonly IObjectMapper _objectMapper = Mock.Of<IObjectMapper>();
    private readonly ILogger<PersonRepository> _logger = Mock.Of<ILogger<PersonRepository>>();

    [Fact]
    public async Task GetPersons_ReturnsPersons()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);

        mock.Setup(m => m.QueryAsync<Person>(It.IsAny<string>(), null, null, null, null))
            .ReturnsAsync(new[] { new Person(), new Person(), new Person() });
        
        // Act
        var repository = new PersonRepository(_objectMapper, _logger);
        var result = (await repository.GetPersons()).ToArray();
        
        // Assert
        mock.Verify(m => m.QueryAsync<Person>(It.IsNotNull<string>(), null, null, null, null), Times.Once);

        result.Should().NotBeNull("because Person rows exist.");
        result.Should().HaveCount(3, "because there exists 3 person rows.");
    }

    [Fact]
    public async Task GetPersonById_ReturnsSingleRow()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);

        mock.Setup(m =>
                m.QuerySingleOrDefaultAsync<Person?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(new Person());

        // Act
        var repository = new PersonRepository(_objectMapper, _logger);
        var result = await repository.GetPersonById(Guid.NewGuid());

        // Assert
        mock.Verify(
            m => m.QuerySingleOrDefaultAsync<Person?>(It.IsNotNull<string>(), It.IsNotNull<object>(), null, null,
                null), Times.Once);
        
        result.Should().NotBeNull("because the Person row exists");
    }
    
    [Fact]
    public async Task GetPersonById_ReturnsNull()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);

        Person? person = null;
        mock.Setup(m =>
                m.QuerySingleOrDefaultAsync<Person?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(person);

        // Act
        var repository = new PersonRepository(_objectMapper, _logger);
        var result = await repository.GetPersonById(Guid.NewGuid());

        // Assert
        mock.Verify(
            m => m.QuerySingleOrDefaultAsync<Person?>(It.IsNotNull<string>(), It.IsNotNull<object>(), null, null,
                null), Times.Once);
        
        result.Should().BeNull("because the Person row does not exist");
    }
}