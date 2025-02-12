using FluentValidation.TestHelper;
using SocialMedia.Application.Queries.GetPost;

namespace SocialMedia.UnitTests.Queries.GetPost;

public class GetPostValidatorTests
{
    private readonly GetPostQueryValidator _validator = new();

    [Fact]
    public async Task QueryValidator_EmptyGuid_ReturnsFail()
    {
        var query = new GetPostQuery(Guid.Empty);

        var result = await _validator.TestValidateAsync(query);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}