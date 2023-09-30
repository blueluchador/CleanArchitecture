using CleanArchitecture.Application.DTOs;

namespace CleanArchitecture.Api.Controllers.Responses;

public class GetPersonsResponse
{
    /// <summary>
    /// List of Persons.
    /// </summary>
    public IEnumerable<Person> Persons { get; set; } = null!;
}