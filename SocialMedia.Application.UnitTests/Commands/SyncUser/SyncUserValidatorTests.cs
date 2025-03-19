using FluentValidation.TestHelper;
using SocialMedia.Application.Commands.SyncUser;

namespace SocialMedia.UnitTests.Commands.SyncUser;

public class SyncUserValidatorTests
{
    private readonly SyncUserValidator _validator = new();

    public static IEnumerable<object[]> ValidData =>
        new List<object[]>
        {
            new object[] { "gewrgeherh", "test", "testergerhherhe rherhehherheh", "test@test.com", "https://test.com" },
            new object[] { "test", "testgergerg ergeerheherh", "test", "test@test.com", "http://test.com" }
        };

    public static IEnumerable<object[]> InvalidUidData =>
        new List<object[]>
        {
            new object[] { string.Empty },
            new object[] { new string('a', 256) }
        };

    public static IEnumerable<object[]> InvalidNameData =>
        new List<object[]>
        {
            new object[] { string.Empty },
            new object[] { new string('a', 101) }
        };

    public static IEnumerable<object[]> InvalidEmailData =>
        new List<object[]>
        {
            new object[] { string.Empty },
            new object[] { "test" },
            new object[] { "test@" + new string('a', 251) },
            new object[] { new string('a', 256) }
        };

    public static IEnumerable<object[]> InvalidImageData =>
        new List<object[]>
        {
            new object[] { string.Empty },
            new object[] { "invalid-url" },
            new object[] { "htp:/invalid-url.com" },
            new object[] { "https://valid.com/" + new string('a', 990) },
            new object[] { new string('a', 256) }
        };

    private static SyncUserCommand CreateCommand(
        string uid = "id",
        string givenName = "test",
        string familyName = "test",
        string email = "test@test.com",
        string image = "https://test.com")
    {
        return new SyncUserCommand(uid, givenName, familyName, email, image);
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task UidValidator_ValidValues_ReturnsSuccess(string uid, string givenName, string familyName,
        string email, string image)
    {
        var command = new SyncUserCommand(uid, givenName, familyName, email, image);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [MemberData(nameof(InvalidUidData))]
    public async Task UidValidator_InvalidValues_ReturnsFail(string invalidUid)
    {
        var command = CreateCommand(invalidUid);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Theory]
    [MemberData(nameof(InvalidNameData))]
    public async Task GivenNameValidator_InvalidValues_ReturnsFail(string invalidGivenName)
    {
        var command = CreateCommand(givenName: invalidGivenName);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.GivenName);
    }

    [Theory]
    [MemberData(nameof(InvalidNameData))]
    public async Task FamilyNameValidator_InvalidValues_ReturnsFail(string invalidFamilyName)
    {
        var command = CreateCommand(familyName: invalidFamilyName);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.FamilyName);
    }

    [Theory]
    [MemberData(nameof(InvalidEmailData))]
    public async Task EmailValidator_InvalidValues_ReturnsFail(string invalidEmail)
    {
        var command = CreateCommand(email: invalidEmail);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Theory]
    [MemberData(nameof(InvalidImageData))]
    public async Task ImageValidator_InvalidValues_ReturnsFail(string invalidImage)
    {
        var command = CreateCommand(image: invalidImage);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Image);
    }
}