using FluentValidation;

namespace SocialMedia.Application.Commands.SyncUser;

public class SyncUserValidator : AbstractValidator<SyncUserCommand>
{
    public SyncUserValidator()
    {
        RuleFor(user => user.Uid)
            .NotEmpty()
            .MaximumLength(255);
        RuleFor(user => user.GivenName)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(user => user.FamilyName)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);
        RuleFor(user => user.Image)
            .NotEmpty()
            .Must(i => Uri.TryCreate(i, UriKind.Absolute, out var uri) &&
                       (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            .MaximumLength(1000);
    }
}