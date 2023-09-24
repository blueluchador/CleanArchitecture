using System.Reflection;

namespace CleanArchitecture.Infrastructure.EmbeddedSqlResources;

internal static class Resource
{
    private static readonly Lazy<string> GetHelloWorldSql = new(() => GetEmbeddedResource("get_hello_world.sql"));
    
    public static string GetHelloWorldQuery => GetHelloWorldSql.Value;
    
    private static string GetEmbeddedResource(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        string resourceName = $"{assembly.GetName().Name}.EmbeddedSqlResources.SqlScripts.{fileName}";
        
        using var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream == null)
        {
            throw new Exception($"The SQL file '{fileName}' was not found.");
        }

        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}