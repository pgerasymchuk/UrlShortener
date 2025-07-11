using Application.URLs.Commands;
using Application.URLs.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/")]
[AllowAnonymous]
public class GuestController(IMediator mediator) : ControllerBase
{
    [HttpPost("shorten")]
    public async Task<IActionResult> ShortenUrl([FromBody] CreateShortenedUrlCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("r/{code}")]
    public async Task<IActionResult> RedirectToOriginal(string code)
    {
        var command = new GetOriginalUrlByShortCodeQuery(code);
        var result = await mediator.Send(command);
        return Redirect(result);
    }
}
