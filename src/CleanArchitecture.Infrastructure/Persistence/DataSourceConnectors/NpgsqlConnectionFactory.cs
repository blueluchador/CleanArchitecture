using System.Data;
using Npgsql;

namespace CleanArchitecture.Infrastructure.Persistence.DataSourceConnectors;

public class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    
    public NpgsqlConnectionFactory(string connectionString)
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