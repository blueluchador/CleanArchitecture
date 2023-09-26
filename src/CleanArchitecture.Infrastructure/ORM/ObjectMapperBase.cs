using System.Data;
using CleanArchitecture.Infrastructure.DataSourceConnectors;
using Dapper;

namespace CleanArchitecture.Infrastructure.ORM;

public abstract class ObjectMapperBase
{
    private readonly IDbConnectionFactory _connectionFactory;

    protected ObjectMapperBase(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null,
        int? commandTimeout = null, CommandType? commandType = null)
    {
        using var conn = await _connectionFactory.CreateConnectionAsync();
        return await conn.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }
}