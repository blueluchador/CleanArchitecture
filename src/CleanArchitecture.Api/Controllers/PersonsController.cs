using CleanArchitecture.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("persons")]
#pragma warning disable CA1825
[Consumes("application/json")]
[Produces("application/json")]
#pragma warning restore CA1825
public class PersonsController : ControllerBase
{
    private readonly IHelloWorldService _helloWorldService;
    
    public PersonsController(IHelloWorldService helloWorldService)
    {
        _helloWorldService = helloWorldService;
    }
    
    /// <summary>
    /// This endpoint returns the person hello world message.
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:guid}/message")]
    public async Task<IActionResult> GetHelloWorldMessage(Guid id)
    {
        return Ok(await _helloWorldService.GetMessage(id));
    }
}