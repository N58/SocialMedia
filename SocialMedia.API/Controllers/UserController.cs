using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Commands.SyncUser;

namespace SocialMedia.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("SyncUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SyncUser(SyncUserCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}