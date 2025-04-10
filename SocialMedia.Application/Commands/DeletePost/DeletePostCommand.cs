using FluentResults;
using MediatR;

namespace SocialMedia.Application.Commands.DeletePost;

public class DeletePostCommand : IRequest<Result>
{
    public required Guid Id { get; set; }
    public required string AuthorId { get; set; }
}