using FluentValidation;

namespace SocialMedia.Application.Queries.GetPost;

public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
{
    public GetPostQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty();
    }
}