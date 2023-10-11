using System.Reflection;

namespace CleanArchitecture.Infrastructure.Persistence.EmbeddedSqlResources;

public static class Resource
{
    private static readonly Lazy<string> GetPersonsSql = new(() => GetEmbeddedResource("get_persons.sql"));
    private static readonly Lazy<string> GetPersonByIdSql = new(() => GetEmbeddedResource("get_person_by_id.sql"));

    public static string GetPersonsSqlQuery => GetPersonsSql.Value;
    public static string GetPersonByIdSqlQuery => GetPersonByIdSql.Value;
    
    private static string GetEmbeddedResource(string fileName)
    {
        string resourceName = $"{MethodBase.GetCurrentMethod()?.DeclaringType?.Namespace}.SqlScripts.{fileName}";
        
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

        if (stream == null)
        {
            throw new Exception($"The SQL file '{fileName}' was not found.");
        }

        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}