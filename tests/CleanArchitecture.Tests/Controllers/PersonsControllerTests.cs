using CleanArchitecture.Api.Controllers;
using CleanArchitecture.Api.Controllers.Requests;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Services;
using CleanArchitecture.TestFixtures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Tests.Controllers;

public class PersonsControllerTests
{
    private readonly IPersonsService _personsService = Mock.Of<IPersonsService>();
    private readonly IHelloWorldService _helloWorldService = Mock.Of<IHelloWorldService>();

    [Fact]
    public async Task GetPersonsRequest_ReturnsSuccess()
    {
        // Arrange
        var mock = Mock.Get(_personsService);

        mock.Setup(m => m.GetPersons()).ReturnsAsync(Fake.CreateMany<Person>());
        
        // Act
        var controller = new PersonsController(_personsService, _helloWorldService);
        var result = await controller.GetPersons(new GetPersonsRequest());
        
        // Assert
        mock.Verify(m => m.GetPersons(), Times.Once);
        
        result.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status200OK,
            "because Person objects exist");
    }

    [Fact]
    public async Task GetPersonById_ReturnsSuccess()
    {
        // Arrange
        var mock = Mock.Get(_personsService);

        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync(Fake.Create<Person>());
        
        // Act
        var controller = new PersonsController(_personsService, _helloWorldService);
        var result = await controller.GetPerson(Guid.NewGuid());
        
        // Assert
        mock.Verify(m => m.GetPersonById(It.IsNotNull<Guid>()), Times.Once);
        
        result.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status200OK,
            "because persons service returns a Person object");
    }
    
    [Fact]
    public async Task GetPersonById_ReturnsNotFound()
    {
        // Arrange
        var mock = Mock.Get(_personsService);

        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync((Person?)null);
        
        // Act
        var controller = new PersonsController(_personsService, _helloWorldService);
        var result = await controller.GetPerson(Guid.NewGuid());
        
        // Assert
        mock.Verify(m => m.GetPersonById(It.IsNotNull<Guid>()), Times.Once);
        
        result.Result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(StatusCodes.Status404NotFound,
            "because persons service returns a null Person object");
    }

    [Fact]
    public async Task AddPerson_Created()
    {
        // Arrange
        var mock = Mock.Get(_personsService);
        
        mock.Setup(m => m.AddPerson(It.IsAny<Person>())).ReturnsAsync(Guid.NewGuid());
        
        // Act
        var controller = new PersonsController(_personsService, _helloWorldService);
        var result = await controller.AddPerson(Fake.Create<AddPersonRequest>());
        
        // Assert
        mock.Verify(m => m.AddPerson(It.IsNotNull<Person>()), Times.Once);

        result.Result.Should().BeOfType<CreatedAtActionResult>().Which.StatusCode.Should()
            .Be(StatusCodes.Status201Created, "because the Person was created.");
    }
    
    [Fact]
    public async Task AddPerson_ReturnsInternalServerError()
    {
        // Arrange
        var mock = Mock.Get(_personsService);

        mock.Setup(m => m.AddPerson(It.IsAny<Person>())).ReturnsAsync((Guid?)null);
        
        // Act
        var controller = new PersonsController(_personsService, _helloWorldService);
        var result = await controller.AddPerson(Fake.Create<AddPersonRequest>());
        
        // Assert
        mock.Verify(m => m.AddPerson(It.IsNotNull<Person>()), Times.Once);

        result.Result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should()
            .Be(StatusCodes.Status500InternalServerError, "because an error occured adding the Person.");
    }

    [Fact]
    public async Task GetHelloWorldMessageRequest_ReturnsSuccess()
    {
        // Arrange
        var mock = Mock.Get(_helloWorldService);

        mock.Setup(m => m.GetMessage(It.IsAny<Guid>())).ReturnsAsync(Fake.Create<HelloWorldMessage>());
        
        // Act
        var controller = new PersonsController(_personsService, _helloWorldService);
        var result = await controller.GetHelloWorldMessage(Guid.NewGuid());
        
        // Assert
        mock.Verify(m => m.GetMessage(It.IsNotNull<Guid>()), Times.Once);

        result.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status200OK,
            "because hello world service returns any message");
    }
}