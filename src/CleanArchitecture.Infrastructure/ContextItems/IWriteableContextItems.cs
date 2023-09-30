namespace CleanArchitecture.Infrastructure.ContextItems;

public interface IWriteableContextItems
{
    void Set(string key, string value);
}