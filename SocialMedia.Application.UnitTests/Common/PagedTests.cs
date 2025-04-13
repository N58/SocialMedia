using Shouldly;
using SocialMedia.Application.Validators;
using SocialMedia.Domain.Common;

namespace SocialMedia.UnitTests.Common;

public class PagedTests
{
    private readonly PagedValidator<int> _intValidator = new();
    private readonly PagedValidator<string> _stringValidator = new();

    [Fact]
    public void Constructor_ShouldInitializeAllProperties()
    {
        // Arrange
        var data = new List<string> { "Item1", "Item2", "Item3" };
        const int totalCount = 10;
        const int page = 2;
        const int size = 3;

        // Act
        var paged = new Paged<string>(data, totalCount, page, size);

        // Assert
        paged.Data.ShouldBe(data);
        paged.TotalCount.ShouldBe(totalCount);
        paged.Page.ShouldBe(page);
        paged.Size.ShouldBe(size);

        var validationResult = _stringValidator.Validate(paged);
        validationResult.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Count_ShouldReturnDataCount()
    {
        // Arrange
        var data = new List<int> { 1, 2, 3, 4, 5 };
        var paged = new Paged<int>(data, 20, 1, 5);

        // Act & Assert
        paged.Count.ShouldBe(5);

        var validationResult = _intValidator.Validate(paged);
        validationResult.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData(10, 3, 4)]
    [InlineData(10, 5, 2)]
    [InlineData(11, 3, 4)]
    [InlineData(0, 5, 0)]
    public void TotalPages_ShouldCalculateCorrectly(int totalCount, int size, int expectedTotalPages)
    {
        // Arrange
        var data = new List<string>();
        var paged = new Paged<string>(data, totalCount, 1, size);

        // Act & Assert
        paged.TotalPages.ShouldBe(expectedTotalPages);

        var validationResult = _stringValidator.Validate(paged);
        validationResult.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Paged_WithEmptyCollection_ShouldHaveCountZero()
    {
        // Arrange
        var data = new List<double>();

        // Act
        var paged = new Paged<double>(data, 0, 1, 10);
        var validator = new PagedValidator<double>();

        // Assert
        paged.Count.ShouldBe(0);
        paged.TotalCount.ShouldBe(0);
        paged.TotalPages.ShouldBe(0);

        var validationResult = validator.Validate(paged);
        validationResult.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Paged_WithDifferentDataCountAndTotalCount_ShouldRespectBoth()
    {
        // Arrange & Act
        var data = new List<int> { 1, 2, 3, 4, 5 };
        var paged = new Paged<int>(data, 20, 1, 5);

        // Assert
        paged.Count.ShouldBe(5);
        paged.TotalCount.ShouldBe(20);
        paged.TotalPages.ShouldBe(4);

        var validationResult = _intValidator.Validate(paged);
        validationResult.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validator_WithNullData_ShouldFailValidation()
    {
        // Arrange
        ICollection<string> data = null!;
        var paged = new Paged<string>(data, 10, 1, 5);

        // Act
        var result = _stringValidator.Validate(paged);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == nameof(paged.Data));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validator_WithInvalidPageSize_ShouldFailValidation(int size)
    {
        // Arrange
        var data = new List<string> { "Item1" };
        var paged = new Paged<string>(data, 10, 1, size);

        // Act
        var result = _stringValidator.Validate(paged);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == nameof(paged.Size));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validator_WithInvalidPageNumber_ShouldFailValidation(int page)
    {
        // Arrange
        var data = new List<string> { "Item1" };
        var paged = new Paged<string>(data, 10, page, 5);

        // Act
        var result = _stringValidator.Validate(paged);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == nameof(paged.Page));
    }

    [Theory]
    [InlineData(-1)]
    public void Validator_WithNegativeTotalCount_ShouldFailValidation(int totalCount)
    {
        // Arrange
        var data = new List<string> { "Item1" };
        var paged = new Paged<string>(data, totalCount, 1, 5);

        // Act
        var result = _stringValidator.Validate(paged);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == nameof(paged.TotalCount));
    }
}