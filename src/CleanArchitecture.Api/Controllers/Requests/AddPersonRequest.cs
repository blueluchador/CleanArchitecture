using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Requests;

public class AddPersonRequest : RequestHeaders
{
    /// <summary>
    /// The Person to add.
    /// </summary>
    [Required]
    [FromBody]
    public Person Person { get; set; }
}