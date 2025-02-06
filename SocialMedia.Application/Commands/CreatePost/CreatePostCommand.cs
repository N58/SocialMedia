using FluentResults;
using MediatR;

namespace SocialMedia.Application.Commands.CreatePost;

public record CreatePostCommand(string Content) : IRequest<Result<Guid>>;
