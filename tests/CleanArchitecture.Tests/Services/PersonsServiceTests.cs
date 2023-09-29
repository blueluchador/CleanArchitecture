using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.TestFixtures;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Tests.Services;

public class PersonsServiceTests
{
    private readonly IPersonRepository _personRepository = Mock.Of<IPersonRepository>();
    private readonly ILogger<PersonsService> _logger = Mock.Of<ILogger<PersonsService>>();

    [Fact]
    public async Task GetPersons_ReturnsPersonObjects()
    {
        // Arrange
        var mock = Mock.Get(_personRepository);
        mock.Setup(m => m.GetPersons()).ReturnsAsync(Fake.CreateMany<Person>(3));
        
        // Act
        var personsService = new PersonsService(_personRepository, _logger);
        var result = (await personsService.GetPersons()).ToArray();
        
        // Assert
        mock.Verify(m => m.GetPersons(), Times.Once);

        result.Should().NotBeEmpty("because Persons exist.");
        result.Should().HaveCount(3, "because there exists 3 persons.");
    }
    
    [Fact]
    public async Task GetPersonById_ReturnsPersonDTO()
    {
        // Arrange
        var person = Fake.Create<Person>();
        var mock = Mock.Get(_personRepository);
        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync(person);
        
        // Act
        var personsService = new PersonsService(_personRepository, _logger);
        var result = await personsService.GetPersonById(person.Uuid);
        
        // Assert
        mock.Verify(m => m.GetPersonById(person.Uuid), Times.Once);
        
        result.Should().NotBeNull("because the Person exists.");
        result?.PersonId.Should().Be(person.Uuid);
        result?.FirstName.Should().Be(person.FirstName);
        result?.LastName.Should().Be(person.LastName);
    }
    
    [Fact]
    public async Task GetPersonById_ReturnsNull()
    {
        // Arrange
        var mock = Mock.Get(_personRepository);
        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync(Fake.CreateNull<Person>());
        
        // Act
        var personsService = new PersonsService(_personRepository, _logger);
        var result = await personsService.GetPersonById(new Guid());
        
        // Assert
        mock.Verify(m => m.GetPersonById(It.IsNotNull<Guid>()), Times.Once);
        
        result.Should().BeNull("because the Person does not exist.");
    }
}