using FluentValidation.TestHelper;
using SocialMedia.Application.Queries.GetPostsPaged;

namespace SocialMedia.UnitTests.Queries.GetPostsPaged;

public class GetPostsPagedValidatorTests
{
    private readonly GetPostsPagedValidator _validator = new();

    [Fact]
    public async Task QueryValidator_PageLessThan_ReturnsFail()
    {
        var query = new GetPostsPagedQuery(0, 5);

        var result = await _validator.TestValidateAsync(query);

        result.ShouldHaveValidationErrorFor(x => x.Page);
    }

    [Fact]
    public async Task QueryValidator_SizeLessThan_ReturnsFail()
    {
        var query = new GetPostsPagedQuery(1, -1);

        var result = await _validator.TestValidateAsync(query);

        result.ShouldHaveValidationErrorFor(x => x.Size);
    }

    [Fact]
    public async Task QueryValidator_SizeGreaterThan_ReturnsFail()
    {
        var query = new GetPostsPagedQuery(1, 50);

        var result = await _validator.TestValidateAsync(query);

        result.ShouldHaveValidationErrorFor(x => x.Size);
    }
}