using FluentValidation;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Commands.CreatePost;

public class CreatePostValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostValidator()
    {
        RuleFor(post => post.Content)
            .NotEmpty().WithMessage(Errors.Post.ContentIsRequired)
            .MinimumLength(3).WithMessage(Errors.Post.ContentExceedsMinLength)
            .MaximumLength(1000).WithMessage(Errors.Post.ContentExceedsMaxLength);
    }
}