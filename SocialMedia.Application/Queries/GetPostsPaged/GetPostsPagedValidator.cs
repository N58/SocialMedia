using FluentValidation;

namespace SocialMedia.Application.Queries.GetPostsPaged;

public class GetPostsPagedValidator : AbstractValidator<GetPostsPagedQuery>
{
    public GetPostsPagedValidator()
    {
        RuleFor(q => q.Page).NotNull().GreaterThanOrEqualTo(1);
        RuleFor(q => q.Size).NotNull().GreaterThan(0).LessThanOrEqualTo(25);
    }
}