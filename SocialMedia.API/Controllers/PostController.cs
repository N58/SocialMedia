using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Attributes;
using SocialMedia.API.Requests;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Application.Commands.DeletePost;
using SocialMedia.Application.Commands.UpdatePost;
using SocialMedia.Application.Dtos;
using SocialMedia.Application.Dtos.Post;
using SocialMedia.Application.Queries.GetPost;
using SocialMedia.Application.Queries.GetPostsPaged;
using SocialMedia.Application.Services;

namespace SocialMedia.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController(IMediator mediator, IMapper mapper, CurrentUserService currentUserService) : ControllerBase
{
    [RequireAuthenticated]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> CreatePost(CreatePostRequest request)
    {
        var command = mapper.Map<CreatePostCommand>(request);
        command.AuthorId = currentUserService.User.Id;
        var result = await mediator.Send(command);

        return CreatedAtAction(
            nameof(CreatePost),
            new { id = result.Value },
            result.Value
        );
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostDto>> GetPost(Guid id)
    {
        var result = await mediator.Send(new GetPostQuery(id));

        var response = mapper.Map<PostDto>(result.Value);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedDto<PostDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostDto>> GetPostsPaged([FromQuery] GetPostsPagedQuery query)
    {
        var result = await mediator.Send(query);

        return Ok(result.Value);
    }

    [RequireAuthenticated]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdatePost(Guid id, UpdatePostRequest request)
    {
        request = request with { Id = id };
        var command = mapper.Map<UpdatePostCommand>(request);
        command.AuthorId = currentUserService.User.Id;
        await mediator.Send(command);

        return Ok();
    }

    [RequireAuthenticated]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeletePost(DeletePostRequest request)
    {
        var command = mapper.Map<DeletePostCommand>(request);
        command.AuthorId = currentUserService.User.Id;
        await mediator.Send(command);

        return Ok();
    }
}