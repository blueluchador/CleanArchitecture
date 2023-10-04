namespace CleanArchitecture.Api.Middleware;

public class Header
{
    public string Key { get; init; } = null!;
    public bool IsRequired { get; init; } = true;
}