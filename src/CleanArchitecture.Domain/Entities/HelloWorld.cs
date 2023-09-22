namespace CleanArchitecture.Domain.Entities;

public class HelloWorld
{
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public string Name { get; set; } = null!;
}