using System.Net;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.IntegrationTests.Extensions;
using CleanArchitecture.TestFixtures;

namespace CleanArchitecture.IntegrationTests.ApiEndpoints;

public class PersonsApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public PersonsApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("b5d74ff1-572f-4dd5-beb3-3aa67adf6b49")]
    [InlineData("53b2abec-a06b-4686-9c5a-5ede268eab6b")]
    public async Task GetHelloWorldMessage_ReturnsSuccess(string personId)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/persons/{personId}/message");
        var message = await response.ProcessResponse<HelloWorldMessage>();
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.OK, "because the endpoint responded successfully");
        message.Should().NotBeNull("because the endpoint returns any message");

        if (personId == "b5d74ff1-572f-4dd5-beb3-3aa67adf6b49")
        {
            message.Message.Should().Be("Hello, Gerardo!", "because the person table returns 'Gerardo'");
        }
        else
        {
            message.Message.Should().Be("Hello, World!", "because the person table returns no rows");
        }
    }
}