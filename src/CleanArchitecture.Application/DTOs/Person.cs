namespace CleanArchitecture.Application.DTOs;

public class Person
{
    public Guid PersonId { get; set; }
    public string FirstName { get; init; } = null!;
    public string? LastName { get; init; }
}