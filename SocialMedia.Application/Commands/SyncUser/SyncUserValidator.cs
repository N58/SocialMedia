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
            .MinimumLength(1)
            .MaximumLength(100);
        RuleFor(user => user.FamilyName)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(100);
        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(1)
            .MaximumLength(255);
        RuleFor(user => user.Image)
            .NotEmpty()
            .Must(i => Uri.TryCreate(i, UriKind.Absolute, out _))
            .When(i => !string.IsNullOrEmpty(i.Image))
            .MinimumLength(1)
            .MaximumLength(1000);
    }
}