namespace CleanArchitecture.Api.Extensions;

public static class AuthorizationExtensions
{
    public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return (app as WebApplication)!.Environment.IsIntegrationTests() ? app : app.UseAuthorization();
    }
}