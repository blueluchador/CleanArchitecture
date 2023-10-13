using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Requests;

public class AddPersonRequest : RequestHeaders
{
    /// <summary>
    /// The Person to add.
    /// </summary>
    [Required]
    [FromBody]
    public PersonPayload Person { get; set; } = null!;
}