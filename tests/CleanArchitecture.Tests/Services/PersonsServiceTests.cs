using CleanArchitecture.Application.Contracts.ContextItems;
using CleanArchitecture.Application.Contracts.Repositories;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.TestFixtures;
using Microsoft.Extensions.Logging;
using PersonDTO = CleanArchitecture.Application.DTOs.Person;

namespace CleanArchitecture.Tests.Services;

public class PersonsServiceTests
{
    private readonly IPersonRepository _personRepository = Mock.Of<IPersonRepository>();
    private readonly IContextItems _contextItems = Mock.Of<IContextItems>();
    private readonly ILogger<PersonsService> _logger = Mock.Of<ILogger<PersonsService>>();

    [Fact]
    public async Task GetPersons_ReturnsPersonObjects()
    {
        // Arrange
        Mock.Get(_contextItems).Setup(m => m.Get(ApiHeaders.TenantId))
            .Returns("ba5eba11-babe-505a-c0bb-dec1a551f1ed");
        
        var mock = Mock.Get(_personRepository);
        mock.Setup(m => m.GetPersons(It.IsAny<Guid>())).ReturnsAsync(Fake.CreateMany<Person>(3));
        
        // Act
        var personsService = new PersonsService(_personRepository, _contextItems, _logger);
        var result = (await personsService.GetPersons()).ToArray();
        
        // Assert
        mock.Verify(m => m.GetPersons(It.IsNotNull<Guid>()), Times.Once);

        result.Should().NotBeEmpty("because Persons exist.");
        result.Should().HaveCount(3, "because there exists 3 persons.");
    }
    
    [Fact]
    public async Task GetPersonById_ReturnsPerson()
    {
        // Arrange
        var person = Fake.Create<Person>();
        var mock = Mock.Get(_personRepository);
        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync(person);
        
        // Act
        var personsService = new PersonsService(_personRepository, _contextItems, _logger);
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
        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync((Person?)null);
        
        // Act
        var personsService = new PersonsService(_personRepository, _contextItems, _logger);
        var result = await personsService.GetPersonById(new Guid());
        
        // Assert
        mock.Verify(m => m.GetPersonById(It.IsNotNull<Guid>()), Times.Once);
        
        result.Should().BeNull("because the Person does not exist.");
    }
    
    [Fact]
    public async Task AddPerson_ReturnsGuid()
    {
        // Arrange
        Mock.Get(_contextItems).Setup(m => m.Get(ApiHeaders.TenantId))
            .Returns("ba5eba11-babe-505a-c0bb-dec1a551f1ed");
        
        var mock = Mock.Get(_personRepository);
        mock.Setup(m => m.AddPerson(It.IsAny<Person>(), It.IsAny<Guid>())).ReturnsAsync(Guid.NewGuid());
        
        // Act
        var personsService = new PersonsService(_personRepository, _contextItems, _logger);
        var result = await personsService.AddPerson(Fake.Create<PersonDTO>());
        
        // Assert
        mock.Verify(m => m.AddPerson(It.IsNotNull<Person>(), It.IsNotNull<Guid>()), Times.Once);
        
        result.Should().NotBeNull("because the adding person to the repo was successful.");
    }
    
    [Fact]
    public async Task AddPerson_ReturnsNull()
    {
        // Arrange
        Mock.Get(_contextItems).Setup(m => m.Get(ApiHeaders.TenantId))
            .Returns("ba5eba11-babe-505a-c0bb-dec1a551f1ed");
        
        var mock = Mock.Get(_personRepository);
        mock.Setup(m => m.AddPerson(It.IsAny<Person>(), It.IsAny<Guid>())).ReturnsAsync((Guid?)null);
        
        // Act
        var personsService = new PersonsService(_personRepository, _contextItems, _logger);
        var result = await personsService.AddPerson(Fake.Create<PersonDTO>());
        
        // Assert
        mock.Verify(m => m.AddPerson(It.IsNotNull<Person>(), It.IsNotNull<Guid>()), Times.Once);
        
        result.Should().BeNull("because there was a problem adding the person to the repo.");
    }
}