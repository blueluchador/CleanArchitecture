using System.Net;
using CleanArchitecture.Api.Controllers.Responses;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Domain.Constants;
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
    [InlineData("ba5eba11-babe-505a-c0bb-dec1a551f1ed", 3)]
    [InlineData("1bad2bad-3bad-4bad-5bad-badbadbadbad", 0)]
    public async Task GetPersons_ReturnsSuccess(string tenantId, int count)
    {
        // Arrange
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add(ApiHeaders.TenantId, tenantId);
        
        // Act
        var response = await client.GetAsync("persons");
        var persons = await response.ProcessResponse<GetPersonsResponse>();
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.OK, "because the endpoint responded successfully");
        persons.Should().NotBeNull("because the endpoint never returns null");
        persons.Persons.Should().HaveCount(count, $"because the endpoint responds with {count} Persons");
    }
    
    [Theory]
    [InlineData("b5d74ff1-572f-4dd5-beb3-3aa67adf6b49", "Buck", "Russell")]
    [InlineData("5ebeb2d5-80fb-4028-89c5-577ca4003ac5", "Austin", "Powers")]
    [InlineData("d8b796f7-b2f1-4ccf-955c-bc5a9f6a6afd", "Rico", "Dynamite")]
    public async Task GetPerson_ReturnsSuccess(string personId, string firstName, string lastName)
    {
        // Arrange
        var client = _factory.CreateClient();
    
        // Act
        var response = await client.GetAsync($"persons/{personId}");
        var person = await response.ProcessResponse<Person>();
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.OK, "because the endpoint responded successfully");
        person.Should().NotBeNull("because the endpoint returns a Person object");

        person.FirstName.Should().Be(firstName, $"because the Persons first name is '{firstName}'");
        person.LastName.Should().Be(lastName, $"because the Persons last name is '{lastName}'");
    }

    [Fact]
    public async Task GetPerson_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("persons/53b2abec-a06b-4686-9c5a-5ede268eab6b");
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound, "because the endpoint did not find a Person");
    }

    [Theory]
    [InlineData("b5d74ff1-572f-4dd5-beb3-3aa67adf6b49", "Buck")]
    [InlineData("5ebeb2d5-80fb-4028-89c5-577ca4003ac5", "Austin")]
    [InlineData("d8b796f7-b2f1-4ccf-955c-bc5a9f6a6afd", "Rico")]
    [InlineData("707a11ed-dead-501e-1bad-70ffeec0ffee", null)]
    public async Task GetHelloWorldMessage_ReturnsSuccess(string personId, string? name)
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
    
    [Theory]
    [InlineData("persons")]
    public async Task Get_EndpointsMissingTenantId_ReturnsBadRequest(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync(url);
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.BadRequest, "because the Tenant ID header is missing");
    }
}