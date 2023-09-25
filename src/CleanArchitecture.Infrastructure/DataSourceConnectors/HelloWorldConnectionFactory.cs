using System.Data;
using Npgsql;

namespace CleanArchitecture.Infrastructure.DataSourceConnectors;

public class HelloWorldConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    
    public HelloWorldConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        
        connection.Open();

        return connection;
    }
    
    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        
        await connection.OpenAsync();

        return connection;
    }
}