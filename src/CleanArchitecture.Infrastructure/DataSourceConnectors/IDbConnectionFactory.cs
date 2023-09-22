using System.Data;

namespace CleanArchitecture.Infrastructure.DataSourceConnectors;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();

    Task<IDbConnection> CreateConnectionAsync();
}