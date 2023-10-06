namespace CleanArchitecture.Api.Middleware;

public class RequestHeaderOptions
{
    public IEnumerable<string> Headers { get; init; } = Array.Empty<string>();
}