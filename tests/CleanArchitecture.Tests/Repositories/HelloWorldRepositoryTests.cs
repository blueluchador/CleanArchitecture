using System.Data;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.DataSourceConnectors;
using CleanArchitecture.Infrastructure.Repositories;
using Dapper;
using Microsoft.Extensions.Logging;
using Moq.Dapper;

namespace CleanArchitecture.Tests.Repositories;

public class HelloWorldRepositoryTests
{
    private readonly IDbConnectionFactory _connectionFactory = Mock.Of<IDbConnectionFactory>();
    private readonly ILogger<HelloWorldRepository> _logger = Mock.Of<ILogger<HelloWorldRepository>>();
    private readonly IDbConnection _dbConnection = Mock.Of<IDbConnection>();

    [Fact]
    public async Task GetHelloWorld_ReturnsSingleRow()
    {
        // Arrange
        Mock.Get(_connectionFactory).Setup(m => m.CreateConnectionAsync()).ReturnsAsync(_dbConnection);

        var mock = Mock.Get(_dbConnection);

        mock.SetupDapperAsync(m =>
                m.QuerySingleOrDefaultAsync<HelloWorld?>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(new HelloWorld());

        // Act
        var repository = new HelloWorldRepository(_connectionFactory, _logger);
        var result = await repository.GetHelloWorld(Guid.NewGuid());

        // Assert
        result.Should().NotBeNull("because the hello world row exists");
    }
}