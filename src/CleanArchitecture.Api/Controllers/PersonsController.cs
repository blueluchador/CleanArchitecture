using CleanArchitecture.Api.Controllers.Requests;
using CleanArchitecture.Api.Controllers.Responses;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Services;
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
    /// This endpoint adds a Person.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<AddPersonResponse>> AddPerson([FromQuery] AddPersonRequest request)
    {
        var id = await _personsService.AddPerson(new Person
        {
            FirstName = request.Person.FirstName,
            LastName = request.Person.LastName
        });
        if (id == null)
        {
            return StatusCode(500);
        }
        
        return CreatedAtAction(nameof(AddPerson), null, new AddPersonResponse { Id = id.Value });
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