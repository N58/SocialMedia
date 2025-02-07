using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Responses.Post;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Application.Extensions;
using SocialMedia.Application.Queries.GetPost;

namespace SocialMedia.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> CreatePost(CreatePostCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsFailed) return result.ToResponse();

        return CreatedAtAction(
            nameof(CreatePost),
            new { id = result.Value },
            result.Value
        );
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostResponse>> GetPost(Guid id)
    {
        var result = await mediator.Send(new GetPostQuery(id));
        if (result.IsFailed) return result.ToResponse();

        var response = mapper.Map<PostResponse>(result.Value);
        return Ok(response);
    }
}