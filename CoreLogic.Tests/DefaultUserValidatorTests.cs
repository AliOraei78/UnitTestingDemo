using Xunit;
using FluentAssertions;

public class DefaultUserValidatorTests
{
    private readonly DefaultUserValidator _validator;

    public DefaultUserValidatorTests()
    {
        // Initialize the validator before each test
        _validator = new DefaultUserValidator();
    }

    [Fact]
    public void IsValidRegistration_ShouldReturnTrue_WhenDataIsValid()
    {
        // Arrange
        string name = "John Doe";
        int age = 25;

        // Act
        bool result = _validator.IsValidRegistration(name, age, out string errorMessage);

        // Assert
        result.Should().BeTrue();
        errorMessage.Should().BeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void IsValidRegistration_ShouldReturnFalse_WhenNameIsInvalid(string invalidName)
    {
        // Arrange
        int age = 20;

        // Act
        bool result = _validator.IsValidRegistration(invalidName, age, out string errorMessage);

        // Assert
        result.Should().BeFalse();
        errorMessage.Should().Be("Name cannot be empty.");
    }

    [Fact]
    public void IsValidRegistration_ShouldReturnFalse_WhenUserIsUnderage()
    {
        // Arrange
        string name = "Alice";
        int age = 17;

        // Act
        bool result = _validator.IsValidRegistration(name, age, out string errorMessage);

        // Assert
        result.Should().BeFalse();
        errorMessage.Should().Be("User must be at least 18 years old.");
    }
}