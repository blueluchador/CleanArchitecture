using System.Data;
using CleanArchitecture.Infrastructure.DataSourceConnectors;

namespace CleanArchitecture.Infrastructure.ORM;

public class HelloWorldObjectMapper : ObjectMapperBase, IObjectMapper
{
    public HelloWorldObjectMapper(IDbConnectionFactory connectionFactory)
        : base(connectionFactory)
    {
    }
}