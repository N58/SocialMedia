using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Attributes;
using SocialMedia.API.Responses;
using SocialMedia.API.Responses.Post;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Application.Commands.DeletePost;
using SocialMedia.Application.Commands.UpdatePost;
using SocialMedia.Application.Queries.GetPost;
using SocialMedia.Application.Queries.GetPostsPaged;

namespace SocialMedia.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [RequireAuthenticated]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> CreatePost(CreatePostCommand command)
    {
        var result = await mediator.Send(command);

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

        var response = mapper.Map<PostResponse>(result.Value);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<PostResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostResponse>> GetPostsPaged([FromQuery] GetPostsPagedQuery query)
    {
        var result = await mediator.Send(query);

        var response = mapper.Map<PagedResponse<PostResponse>>(result.Value);
        return Ok(response);
    }

    [RequireAuthenticated]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdatePost(Guid id, UpdatePostCommand command)
    {
        command = command with { Id = id };
        await mediator.Send(command);

        return Ok();
    }

    [RequireAuthenticated]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeletePost(DeletePostCommand command)
    {
        await mediator.Send(command);

        return Ok();
    }
}