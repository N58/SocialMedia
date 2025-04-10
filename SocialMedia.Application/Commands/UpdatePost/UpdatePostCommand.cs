using FluentResults;
using MediatR;

namespace SocialMedia.Application.Commands.UpdatePost;

public class UpdatePostCommand : IRequest<Result>
{
    public required Guid Id { get; set; }
    public required string Content { get; set; }
    public required string AuthorId { get; set; }
}