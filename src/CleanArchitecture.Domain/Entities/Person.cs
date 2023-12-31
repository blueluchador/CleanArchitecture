namespace CleanArchitecture.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public string? FirstName { get; init; }
    public string LastName { get; set; } = null!;
}