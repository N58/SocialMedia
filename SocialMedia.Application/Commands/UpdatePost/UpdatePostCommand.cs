using FluentResults;
using MediatR;

namespace SocialMedia.Application.Commands.UpdatePost;

public record UpdatePostCommand(Guid Id, string Content) : IRequest<Result>;