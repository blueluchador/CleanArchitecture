using CleanArchitecture.Application.Contracts.ContextItems;
using CleanArchitecture.Domain.Constants;
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
    private readonly IContextItems _contextItems = Mock.Of<IContextItems>();
    private readonly ILogger<PersonRepository> _logger = Mock.Of<ILogger<PersonRepository>>();

    [Fact]
    public async Task GetPersons_ReturnsPersons()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);

        Mock.Get(_contextItems).Setup(m => m.Get(ApiHeaders.TenantId))
            .Returns("ba5eba11-babe-505a-c0bb-dec1a551f1ed");

        mock.Setup(m => m.QueryAsync<Person>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(Fake.CreateMany<Person>(3));
        
        // Act
        var repository = new PersonRepository(_objectMapper, _contextItems, _logger);
        var result = (await repository.GetPersons()).ToArray();
        
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
                m.QuerySingleOrDefaultAsync<Person?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(Fake.Create<Person>());

        // Act
        var repository = new PersonRepository(_objectMapper, _contextItems, _logger);
        var result = await repository.GetPersonById(Guid.NewGuid());

        // Assert
        mock.Verify(
            m => m.QuerySingleOrDefaultAsync<Person?>(Resource.GetPersonByIdSqlQuery, It.IsNotNull<object>(), null,
                null, null), Times.Once);
        
        result.Should().NotBeNull("because the Person row exists");
    }
    
    [Fact]
    public async Task GetPersonById_ReturnsNull()
    {
        // Arrange
        var mock = Mock.Get(_objectMapper);
        
        mock.Setup(m =>
                m.QuerySingleOrDefaultAsync<Person?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(Fake.CreateNull<Person>());

        // Act
        var repository = new PersonRepository(_objectMapper, _contextItems, _logger);
        var result = await repository.GetPersonById(Guid.NewGuid());

        // Assert
        mock.Verify(
            m => m.QuerySingleOrDefaultAsync<Person?>(Resource.GetPersonByIdSqlQuery, It.IsNotNull<object>(), null,
                null, null), Times.Once);
        
        result.Should().BeNull("because the Person row does not exist");
    }
}