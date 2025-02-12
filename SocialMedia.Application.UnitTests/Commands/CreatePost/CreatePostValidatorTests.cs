using FluentValidation.TestHelper;
using SocialMedia.Application.Commands.CreatePost;

namespace SocialMedia.UnitTests.Commands.CreatePost;

public class CreatePostValidatorTests
{
    private readonly CreatePostValidator _validator = new();

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public async Task ContentValidator_ContentIsEmpty_ReturnsFail(string content)
    {
        var command = new CreatePostCommand(content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public async Task ContentValidator_ContentHasMinimumLength_ReturnsFail()
    {
        var content = new string('a', 2);
        var command = new CreatePostCommand(content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public async Task ContentValidator_ContentExceedsMaxLength_ReturnsFail()
    {
        var content = new string('a', 1001);
        var command = new CreatePostCommand(content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public async Task ContentValidator_ContentIsValid_ReturnsOk()
    {
        var content = new string('a', 1000);
        var command = new CreatePostCommand(content);

        var result = await _validator.TestValidateAsync(command);
        result.ShouldNotHaveValidationErrorFor(c => c.Content);
    }
}