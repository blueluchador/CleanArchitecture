using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Requests;

// Note: Some headers are handled in the middleware and accessible in IContextItems.
// You can also access them here, but the main purpose is for Open API documentation.
public class RequestHeaders
{
    /// <summary>
    /// The Tenant ID.
    /// </summary>
    [Required]
    [FromHeader(Name = ApiHeaders.TenantId)]
    public Guid TenantId { get; set; }
}