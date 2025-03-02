using FluentValidation.TestHelper;
using SocialMedia.Application.Commands.UpdatePost;

namespace SocialMedia.UnitTests.Commands.UpdatePost;

public class UpdatePostValidatorTests
{
    private readonly UpdatePostValidator _validator = new();

    [Fact]
    public async Task QueryValidator_EmptyGuid_ReturnsFail()
    {
        var command = new UpdatePostCommand(Guid.Empty, "test test");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public async Task ContentValidator_ContentIsEmpty_ReturnsFail(string content)
    {
        var command = new UpdatePostCommand(Guid.NewGuid(), content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public async Task ContentValidator_ContentHasMinimumLength_ReturnsFail()
    {
        var content = new string('a', 2);
        var command = new UpdatePostCommand(Guid.NewGuid(), content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public async Task ContentValidator_ContentExceedsMaxLength_ReturnsFail()
    {
        var content = new string('a', 1001);
        var command = new UpdatePostCommand(Guid.NewGuid(), content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public async Task ContentValidator_ContentIsValid_ReturnsOk()
    {
        var content = new string('a', 1000);
        var command = new UpdatePostCommand(Guid.NewGuid(), content);

        var result = await _validator.TestValidateAsync(command);
        result.ShouldNotHaveValidationErrorFor(c => c.Content);
    }
}