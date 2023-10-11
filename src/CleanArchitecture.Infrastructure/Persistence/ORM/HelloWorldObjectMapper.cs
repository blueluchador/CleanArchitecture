using CleanArchitecture.Infrastructure.Persistence.DataSourceConnectors;

namespace CleanArchitecture.Infrastructure.Persistence.ORM;

public class HelloWorldObjectMapper : ObjectMapperBase, IObjectMapper
{
    public HelloWorldObjectMapper(IDbConnectionFactory connectionFactory)
        : base(connectionFactory)
    {
    }
}