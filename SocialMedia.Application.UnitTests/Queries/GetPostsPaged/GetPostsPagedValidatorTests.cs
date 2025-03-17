using FluentValidation.TestHelper;
using SocialMedia.Application.Queries.GetPostsPaged;

namespace SocialMedia.UnitTests.Queries.GetPostsPaged;

public class GetPostsPagedValidatorTests
{
    private readonly GetPostsPagedValidator _validator = new();

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task QueryValidator_InvalidPage_ShouldHaveValidationError(int page)
    {
        var query = new GetPostsPagedQuery(page, 5);

        var result = await _validator.TestValidateAsync(query);

        result.ShouldHaveValidationErrorFor(x => x.Page);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(50)]
    public async Task QueryValidator_InvalidSize_ShouldHaveValidationError(int size)
    {
        var query = new GetPostsPagedQuery(1, size);

        var result = await _validator.TestValidateAsync(query);

        result.ShouldHaveValidationErrorFor(x => x.Size);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(5, 10)]
    [InlineData(10, 20)]
    public async Task QueryValidator_ValidPageAndSize_ShouldNotHaveValidationError(int page, int size)
    {
        var query = new GetPostsPagedQuery(page, size);

        var result = await _validator.TestValidateAsync(query);

        result.ShouldNotHaveValidationErrorFor(x => x.Page);
        result.ShouldNotHaveValidationErrorFor(x => x.Size);
    }
}