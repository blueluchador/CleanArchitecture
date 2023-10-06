using CleanArchitecture.Api.Controllers.Requests;
using CleanArchitecture.Api.Controllers.Responses;
using CleanArchitecture.Application.Contracts.Services;
using CleanArchitecture.Application.DTOs;
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
    private readonly IPersonsService _personsService;
    private readonly IHelloWorldService _helloWorldService;
    
    public PersonsController(IPersonsService personsService, IHelloWorldService helloWorldService)
    {
        _helloWorldService = helloWorldService;
        _personsService = personsService;
    }

    /// <summary>
    /// This endpoint returns all persons.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<GetPersonsResponse>> GetPersons([FromQuery] GetPersonsRequest request)
    {
        return Ok(new GetPersonsResponse
        {
            Persons = await _personsService.GetPersons()
        });
    }

    /// <summary>
    /// This endpoint returns a Person.
    /// </summary>
    /// <param name="id">The Person ID.</param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Person>> GetPerson(Guid id)
    {
        var person = await _personsService.GetPersonById(id);
        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }
    
    /// <summary>
    /// This endpoint returns the hello world message.
    /// </summary>
    /// <param name="id">The Person ID.</param>
    /// <returns></returns>
    [HttpGet("{id:guid}/message")]
    public async Task<ActionResult<HelloWorldMessage>> GetHelloWorldMessage(Guid id)
    {
        return Ok(await _helloWorldService.GetMessage(id));
    }
}