using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.ContextItems;

public static class ContextItemsExtensions
{
    public static IServiceCollection AddContextItemsService(this IServiceCollection services)
    {
        return services
            .AddScoped<ContextItems>()
            .AddScoped<IContextItems>(s => s.GetRequiredService<ContextItems>())
            .AddScoped<IWriteableContextItems>(s => s.GetRequiredService<ContextItems>());
    }
}