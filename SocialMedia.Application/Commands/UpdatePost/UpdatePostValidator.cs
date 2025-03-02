using FluentValidation;

namespace SocialMedia.Application.Commands.UpdatePost;

public class UpdatePostValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostValidator()
    {
        RuleFor(post => post.Id)
            .NotEmpty();
        RuleFor(post => post.Content)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(1000);
    }
}