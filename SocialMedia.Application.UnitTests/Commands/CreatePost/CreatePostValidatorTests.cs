using FluentValidation.TestHelper;
using SocialMedia.Application.Commands.CreatePost;

namespace SocialMedia.UnitTests.Commands.CreatePost;

public class CreatePostValidatorTests
{
    private readonly CreatePostValidator _validator = new();

    public static IEnumerable<object[]> InvalidContentData =>
        new List<object[]>
        {
            new object[] { " " },
            new object[] { "" },
            new object[] { new string('a', 2) },
            new object[] { new string('a', 1001) }
        };

    [Theory]
    [MemberData(nameof(InvalidContentData))]
    public async Task ContentValidator_InvalidContent_ShouldHaveValidationError(string content)
    {
        var command = new CreatePostCommand
        {
            Content = content,
            AuthorId = "12345"
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(500)]
    [InlineData(1000)]
    public async Task ContentValidator_ValidContent_ShouldNotHaveValidationError(int length)
    {
        var content = new string('a', length);
        var command = new CreatePostCommand
        {
            Content = content,
            AuthorId = "12345"
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Content);
    }
}