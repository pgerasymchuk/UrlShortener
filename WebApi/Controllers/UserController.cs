using System.Security.Claims;
using Application.URLs.Commands;
using Application.URLs.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/urls")]
[Authorize(Policy = "UserPolicy")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("my")]
    public async Task<IActionResult> GetMyUrls()
    {
        var result = await mediator.Send(new GetMyUrlsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetUrlByIdQuery(id));
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var result = await mediator.Send(new DeleteUrlCommand(id));
        return NoContent();
    }
}
