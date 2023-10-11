using System.Data;

namespace CleanArchitecture.Infrastructure.Persistence.DataSourceConnectors;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();

    Task<IDbConnection> CreateConnectionAsync();
}