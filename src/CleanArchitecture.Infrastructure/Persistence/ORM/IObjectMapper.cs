using System.Data;

namespace CleanArchitecture.Infrastructure.Persistence.ORM;

public interface IObjectMapper
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null,
        int? commandTimeout = null, CommandType? commandType = null) where T : class;
    
    Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null,
        int? commandTimeout = null, CommandType? commandType = null) where T : class;

    Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null,
        int? commandTimeout = null, CommandType? commandType = null) where T : class;
}