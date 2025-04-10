using FluentValidation;

namespace SocialMedia.Application.Queries.GetUserByUserId;

public class GetUserByUserIdQueryValidator : AbstractValidator<GetUserByUserIdQuery>
{
    public GetUserByUserIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty();
    }
}