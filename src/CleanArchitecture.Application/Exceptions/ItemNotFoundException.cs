namespace CleanArchitecture.Application.Exceptions;

public class ItemNotFoundException : NotFoundException
{
    public ItemNotFoundException(string name, Guid id)
        : base($"The {name} item with ID '{id}' was not found")
    {

    }
}