using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence.EmbeddedSqlResources;
using CleanArchitecture.Infrastructure.Persistence.ORM;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using CleanArchitecture.TestFixtures;
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

        mock.Setup(m => m.QueryAsync<Person>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(Fake.CreateMany<Person>(3));
        
        // Act
        var repository = new PersonRepository(_objectMapper, _logger);
        var result = (await repository.GetPersons(Guid.NewGuid())).ToArray();
        
        // Assert
        mock.Verify(m => m.QueryAsync<Person>(Resource.GetPersonsSqlQuery, It.IsNotNull<object>(), null, null, null),
            Times.Once);

        result.Should().NotBeEmpty("because Person rows exist.");
        result.Should().HaveCount(3, "because there exists 3 person rows.");
    }

    [Fact]
    public async Task GetPersonById_ReturnsSingleRow()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);

        mock.Setup(m =>
                m.QuerySingleOrDefaultAsync<Person>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(Fake.Create<Person>());

        // Act
        var repository = new PersonRepository(_objectMapper, _logger);
        var result = await repository.GetPersonById(Guid.NewGuid());

        // Assert
        mock.Verify(
            m => m.QuerySingleOrDefaultAsync<Person>(Resource.GetPersonByIdSqlQuery, It.IsNotNull<object>(), null,
                null, null), Times.Once);
        
        result.Should().NotBeNull("because the Person row exists");
    }
    
    [Fact]
    public async Task GetPersonById_ReturnsNull()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);

        Person? person = null;
        mock.Setup(m =>
                m.QuerySingleOrDefaultAsync<Person>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))!
            .ReturnsAsync(person);

        // Act
        var repository = new PersonRepository(_objectMapper, _logger);
        var result = await repository.GetPersonById(Guid.NewGuid());

        // Assert
        mock.Verify(
            m => m.QuerySingleOrDefaultAsync<Person>(Resource.GetPersonByIdSqlQuery, It.IsNotNull<object>(), null, null,
                null), Times.Once);
        
        result.Should().BeNull("because the Person row does not exist");
    }
    
    [Fact]
    public async Task AddPerson_ReturnsGuid()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);

        mock.Setup(m => m.QuerySingleAsync<Person>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))!
            .ReturnsAsync(Fake.Create<Person>());

        // Act
        var repository = new PersonRepository(_objectMapper, _logger);
        var result = await repository.AddPerson(Fake.Create<Person>(), Guid.NewGuid());

        // Assert
        mock.Verify(
            m => m.QuerySingleAsync<Person>(Resource.AddPersonSqlQuery, It.IsNotNull<object>(), null, null, null),
            Times.Once);

        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task AddPerson_ReturnsNull()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);

        mock.Setup(m => m.QuerySingleAsync<Person>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))!
            .ThrowsAsync(new Exception());

        // Act
        var repository = new PersonRepository(_objectMapper, _logger);
        var result = await repository.AddPerson(Fake.Create<Person>(), Guid.NewGuid());

        // Assert
        mock.Verify(
            m => m.QuerySingleAsync<Person>(Resource.AddPersonSqlQuery, It.IsNotNull<object>(), null, null, null),
            Times.Once);

        result.Should().BeNull();
    }
}