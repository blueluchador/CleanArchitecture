using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Api.Controllers.Requests;

public class PersonPayload
{
    [MaxLength(40)]
    public string FirstName { get; init; } = null!;
    
    [MaxLength(40)]
    public string? LastName => null;
}