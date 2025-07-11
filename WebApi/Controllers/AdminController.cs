using Application.URLs.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "AdminPolicy")]
public class AdminController(IMediator mediator) : ControllerBase
{
    [HttpGet("urls")]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllUrlsQuery());
        return Ok(result);
    }
}
