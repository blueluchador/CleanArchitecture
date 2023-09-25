using System.Data;

namespace CleanArchitecture.Infrastructure.ORM;

public interface IObjectMapper
{
    Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null,
        int? commandTimeout = null, CommandType? commandType = null);
}