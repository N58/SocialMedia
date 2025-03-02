using FluentResults;
using MediatR;

namespace SocialMedia.Application.Commands.DeletePost;

public record DeletePostCommand(Guid Id) : IRequest<Result>;