using CleanArchitecture.Application.Contracts.ContextItems;

namespace CleanArchitecture.Infrastructure.ContextItems;

public class ContextItems : IContextItems, IWriteableContextItems
{
    private readonly IDictionary<string, string> _contextItems = new Dictionary<string, string>();
    
    public bool Contains(string key)
    {
        return _contextItems.ContainsKey(key);
    }

    public string Get(string key)
    {
        return _contextItems[key];
    }

    public void Set(string key, string value)
    {
        _contextItems[key] = value;
    }
}