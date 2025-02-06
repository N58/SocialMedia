using FluentValidation;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Validators;

public class PostValidator : AbstractValidator<CreatePostCommand>
{
    public PostValidator()
    {
        RuleFor(post => post.Content)
            .NotEmpty().WithMessage(Errors.Post.ContentIsRequired)
            .MaximumLength(1000).WithMessage(Errors.Post.ContentCannotExceed);
    }
}