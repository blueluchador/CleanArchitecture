namespace CleanArchitecture.Api.Extensions;

public static class EnvironmentExtensions
{
    public static bool IsIntegrationTests(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.IsEnvironment("IntegrationTests");
    }
}