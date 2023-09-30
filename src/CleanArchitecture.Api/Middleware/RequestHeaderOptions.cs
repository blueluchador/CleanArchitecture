namespace CleanArchitecture.Api.Middleware;

public class RequestHeaderOptions
{
    public IEnumerable<string> Headers { get; set; } = Array.Empty<string>();
}