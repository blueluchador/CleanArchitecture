using CleanArchitecture.Api.Controllers;
using CleanArchitecture.Application.Contracts.Services;
using CleanArchitecture.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Tests.Controllers;

public class PersonsControllerTests
{
    private readonly IHelloWorldService _helloWorldService = Mock.Of<IHelloWorldService>();

    [Fact]
    public async Task GetHelloWorldMessageRequest_Success()
    {
        // Arrange
        var mock = Mock.Get(_helloWorldService);

        mock.Setup(m => m.GetMessage(It.IsAny<Guid>())).ReturnsAsync(new HelloWorldMessage());
        
        // Act
        var controller = new PersonsController(_helloWorldService);
        var result = await controller.GetHelloWorldMessage(Guid.NewGuid());
        
        // Assert
        mock.Verify(m => m.GetMessage(It.IsNotNull<Guid>()), Times.Once);

        result.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status200OK,
            "because hello world service returns any message");
    }
}