using CleanArchitecture.Api.Controllers;
using CleanArchitecture.Api.Controllers.Requests;
using CleanArchitecture.Application.Contracts.Services;
using CleanArchitecture.Application.DTOs;
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
            "because persons service returns a person object");
    }
    
    [Fact]
    public async Task GetPersonById_ReturnsNotFound()
    {
        // Arrange
        var mock = Mock.Get(_personsService);

        mock.Setup(m => m.GetPersonById(It.IsAny<Guid>())).ReturnsAsync(Fake.CreateNull<Person>());
        
        // Act
        var controller = new PersonsController(_personsService, _helloWorldService);
        var result = await controller.GetPerson(Guid.NewGuid());
        
        // Assert
        mock.Verify(m => m.GetPersonById(It.IsNotNull<Guid>()), Times.Once);
        
        result.Result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(StatusCodes.Status404NotFound,
            "because persons service returns a null person object");
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