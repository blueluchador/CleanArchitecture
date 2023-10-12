using System.Data;
using CleanArchitecture.Infrastructure.Persistence.DataSourceConnectors;
using Dapper;

namespace CleanArchitecture.Infrastructure.Persistence.ORM;

public abstract class ObjectMapperBase
{
    private readonly IDbConnectionFactory _connectionFactory;

    protected ObjectMapperBase(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null,
        IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
    {
        using var conn = await _connectionFactory.CreateConnectionAsync();
        return await conn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null,
        IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
    {
        using var conn = await _connectionFactory.CreateConnectionAsync();
        return await conn.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }
}