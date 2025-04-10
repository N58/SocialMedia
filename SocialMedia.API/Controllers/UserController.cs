using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Attributes;
using SocialMedia.API.Responses.User;
using SocialMedia.Application.Services;
using SocialMedia.Domain.Entities;

namespace SocialMedia.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMapper mapper, CurrentUserService currentUserService) : ControllerBase
{
    [RequireAuthenticated]
    [HttpGet("me")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Me()
    {
        var user = currentUserService.User;
        var responseUser = mapper.Map<UserResponse>(user);
        return Ok(responseUser);
    }
}