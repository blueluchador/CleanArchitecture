using System.Net;
using System.Text;
using CleanArchitecture.Api.Controllers.Requests;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.TestFixtures;
using Newtonsoft.Json;

namespace CleanArchitecture.IntegrationTests.ApiEndpoints;

public class ApiErrorTests : IClassFixture<BadWebApplicationFactory>
{
    private readonly BadWebApplicationFactory _factory;

    public ApiErrorTests(BadWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("persons")]
    [InlineData("persons/df92f01b-3516-4a86-8ee6-bd5e7ddd749b")]
    [InlineData("persons/df92f01b-3516-4a86-8ee6-bd5e7ddd749b/message")]
    public async Task Get_ReturnsInternalServerError(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add(ApiHeaders.TenantId, Guid.NewGuid().ToString());
        
        // Act
        var response = await client.GetAsync(url);
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.InternalServerError, "because external services are not running.");
    }

    [Fact]
    public async Task AddPersonContent_ReturnsInternalServerError()
    {
        // Arrange
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add(ApiHeaders.TenantId, Guid.NewGuid().ToString());
        
        // Act
        var response = await client.PostAsync("persons",
            new StringContent(JsonConvert.SerializeObject(new PersonPayload { FirstName = "Gerardo" }), Encoding.UTF8,
                "application/json"));
        
        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.InternalServerError, "because external services are not running.");
    }
}