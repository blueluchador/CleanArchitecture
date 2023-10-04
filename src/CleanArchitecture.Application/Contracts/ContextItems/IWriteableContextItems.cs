namespace CleanArchitecture.Application.Contracts.ContextItems;

public interface IWriteableContextItems
{
    void Set(string key, string value);
}