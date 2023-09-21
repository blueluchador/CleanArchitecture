namespace CleanArchitecture.Web.Extensions;

public static class HealthChecksExtensions
{
    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddHealthChecks().AddNpgSql(configuration.GetConnectionString("CleanArchitectureDB"));
        return services;
    }

    public static IApplicationBuilder UseCustomHealthChecks(this IApplicationBuilder app)
    {
        return app.UseHealthChecks("/health");
    }
}