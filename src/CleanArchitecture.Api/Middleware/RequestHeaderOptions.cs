namespace CleanArchitecture.Api.Middleware;

public class RequestHeaderOptions
{
    public IEnumerable<Header> Headers { get; init; } = Array.Empty<Header>();
}