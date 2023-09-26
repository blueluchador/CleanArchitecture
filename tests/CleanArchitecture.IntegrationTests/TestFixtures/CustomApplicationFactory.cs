using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.IntegrationTests.TestFixtures;

public class CustomApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTests");
        return base.CreateHost(builder);
    }
    //
    // public IDbConnectionFactory DbConnectionFactory =>
    //     Services.GetService<IDbConnectionFactory>() ?? throw new InvalidOperationException();
}