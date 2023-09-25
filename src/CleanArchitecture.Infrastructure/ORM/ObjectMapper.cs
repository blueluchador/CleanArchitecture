using System.Data;
using CleanArchitecture.Infrastructure.DataSourceConnectors;
using Dapper;

namespace CleanArchitecture.Infrastructure.ORM;

public class ObjectMapper : IObjectMapper
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ObjectMapper(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null,
        int? commandTimeout = null, CommandType? commandType = null)
    {
        using var conn = await _connectionFactory.CreateConnectionAsync();
        return await conn.QuerySingleOrDefaultAsync(sql, param, transaction, commandTimeout, commandType);
    }
}