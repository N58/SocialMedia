using FluentValidation;
using SocialMedia.Domain.Common;

namespace SocialMedia.Application.Validators;

public class PagedValidator<T> : AbstractValidator<Paged<T>>
{
    public PagedValidator()
    {
        RuleFor(x => x.Data)
            .NotNull();

        RuleFor(x => x.Size)
            .GreaterThan(0);

        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.TotalCount)
            .GreaterThanOrEqualTo(0);
    }
}
