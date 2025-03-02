using FluentValidation;

namespace SocialMedia.Application.Commands.DeletePost;

public class DeletePostValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostValidator()
    {
        RuleFor(post => post.Id)
            .NotEmpty()
            .Must(id => Guid.Empty != id);
    }
}