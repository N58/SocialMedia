using FluentResults;
using MediatR;

namespace SocialMedia.Application.Commands.SyncUser;

public record SyncUserCommand(string Uid, string GivenName, string FamilyName, string Email, string Image)
    : IRequest<Result<Unit>>;