using FluentValidation;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Commands.CreatePost;

public class CreatePostValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostValidator()
    {
        RuleFor(post => post.Content)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(1000);
    }
}