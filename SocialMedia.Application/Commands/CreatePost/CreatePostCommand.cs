using FluentResults;
using MediatR;

namespace SocialMedia.Application.Commands.CreatePost;

public class CreatePostCommand : IRequest<Result<Guid>>
{
    public required string Content { get; set; }
    public required string AuthorId { get; set; }
}