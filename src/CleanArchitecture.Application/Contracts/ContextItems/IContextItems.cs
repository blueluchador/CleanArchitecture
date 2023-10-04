namespace CleanArchitecture.Application.Contracts.ContextItems;

public interface IContextItems
{
    bool Contains(string key);

    string Get(string key);
}