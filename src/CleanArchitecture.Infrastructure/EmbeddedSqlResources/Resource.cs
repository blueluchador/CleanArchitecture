using System.Reflection;

namespace CleanArchitecture.Infrastructure.EmbeddedSqlResources;

internal static class Resource
{
    private static string GetEmbeddedResource(string fileName)
    {
        string filePath = $"CleanArchitecture.Infrastructure.EmbeddedSqlResources.SqlScripts.{fileName}";

        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filePath);

        if (stream == null)
        {
            throw new Exception($"The SQL file '{fileName}' was not found.");
        }

        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}