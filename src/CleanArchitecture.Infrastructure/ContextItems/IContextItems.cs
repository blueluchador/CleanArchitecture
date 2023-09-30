namespace CleanArchitecture.Infrastructure.ContextItems;

public interface IContextItems
{
    bool Contains(string key);

    string Get(string key);
}