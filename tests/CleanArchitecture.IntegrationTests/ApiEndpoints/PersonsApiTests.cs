using System.Net;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.IntegrationTests.TestFixtures;

namespace CleanArchitecture.IntegrationTests.ApiEndpoints;

public class PersonsApiTests : IClassFixture<CustomApplicationFactory>
{
    private readonly HttpClient _client;

    public PersonsApiTests(CustomApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetHelloWorldMessage_ReturnsMessage()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/persons/b5d74ff1-572f-4dd5-beb3-3aa67adf6b49/message");
        var message = await response.ProcessResponse<HelloWorldMessage>();
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.OK);

        message.Should().NotBeNull("because the endpoint returns any message");
        message.Message.Should().Be("Hello, Gerardo!", "because the person table returns 'Gerardo'");
    }
    
    [Fact]
    public async Task GetHelloWorldMessage_ReturnsDefaultMessage()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/persons/53b2abec-a06b-4686-9c5a-5ede268eab6b/message");
        var message = await response.ProcessResponse<HelloWorldMessage>();
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.OK);

        message.Should().NotBeNull("because the endpoint returns any message");
        message.Message.Should().Be("Hello, World!", "because the person table returns no rows");
    }
}