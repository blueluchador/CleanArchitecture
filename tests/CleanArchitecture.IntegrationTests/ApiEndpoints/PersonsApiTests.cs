using System.Net;
using CleanArchitecture.Api.Controllers.Responses;
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

    [Fact]
    public async Task GetPersons_Success()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("persons");
        var persons = await response.ProcessResponse<GetPersonsResponse>();
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.OK, "because the endpoint responded successfully");
        persons.Should().NotBeNull("because the endpoint never returns null");
        persons.Persons.Should().HaveCount(3, "because the endpoint responds with 3 Persons");
    }

    [Theory]
    [InlineData("b5d74ff1-572f-4dd5-beb3-3aa67adf6b49", "Buck")]
    [InlineData("5ebeb2d5-80fb-4028-89c5-577ca4003ac5", "Austin")]
    [InlineData("d8b796f7-b2f1-4ccf-955c-bc5a9f6a6afd", "Rico")]
    [InlineData("53b2abec-a06b-4686-9c5a-5ede268eab6b", null)]
    public async Task GetHelloWorldMessage_Success(string personId, string? name)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"persons/{personId}/message");
        var message = await response.ProcessResponse<HelloWorldMessage>();
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.OK, "because the endpoint responded successfully");
        message.Should().NotBeNull("because the endpoint returns any message");

        if (name != null)
        {
            message.Message.Should().Be($"Hello, {name}!", $"because the person table returns '{name}'");
        }
        else
        {
            message.Message.Should().Be("Hello, World!", "because the person table returns no rows");
        }
    }
}