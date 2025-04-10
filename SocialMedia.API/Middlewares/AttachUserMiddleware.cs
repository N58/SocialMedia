using System.Security.Claims;
using AutoMapper;
using MediatR;
using SocialMedia.Application.Commands.SyncUser;
using SocialMedia.Application.Queries.GetUserByUserId;
using SocialMedia.Application.Services;

namespace SocialMedia.API.Middlewares;

public sealed class AttachUserMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IMediator mediator, IMapper mapper, CurrentUserService currentUserService)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var userResult = await mediator.Send(new GetUserByUserIdQuery(userId));
            
            if (userResult is { IsSuccess: true, Value: not null })
            {
                currentUserService.User = userResult.Value;
            }
            else
            {
                await SyncUser(mediator, context.User);
                userResult = await mediator.Send(new GetUserByUserIdQuery(userId));
                
                if (userResult is { IsSuccess: true, Value: not null })
                {
                    currentUserService.User = userResult.Value;
                }
            }
        }
        
        await next(context);
    }
    
    private static async Task SyncUser(IMediator mediator, ClaimsPrincipal user)
    {
        await mediator.Send(new SyncUserCommand(
            user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
            user.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
            user.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
            user.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            user.FindFirstValue("picture") ?? string.Empty
        ));
    }
}