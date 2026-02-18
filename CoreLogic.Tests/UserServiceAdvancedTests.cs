using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using CoreLogic;
using FluentAssertions;
using Moq;
using Xunit;
using static CoreLogic.Tests.UserServiceTests;

namespace CoreLogic.Tests;

public class UserServiceAdvancedTests
{
    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] objects)
            : base(new AutoMoqDataAttribute(), objects)
        {
        }
    }

    [Theory]
    [AutoMoqData]  // Use AutoMoqCustomization for automatic mock injection
    public void RegisterAndGetUser_ValidComplexFlow_ReturnsCorrectInfo(
        [Frozen] Mock<IUserRepository> mockRepo,  // Frozen mock
        UserService service,
        Fixture fixture)  // Fixture for data generation
    {
        // Arrange - generate complex data using AutoFixture
        var validUser = fixture
            .Build<User>()
            .With(u => u.Age, fixture.Create<int>() % 50 + 18)  // Valid age 18–67
            .With(u => u.Name, "Valid_" + fixture.Create<string>().Substring(0, 8))
            .Create();

        mockRepo
            .Setup(r => r.SaveUser(It.Is<User>(u =>
                u.Name == validUser.Name && u.Age == validUser.Age)))
            .Returns(true);

        mockRepo
            .SetupSequence(r => r.GetUserById(It.IsAny<int>()))
            .Returns((User?)null) 
            .Returns(validUser);

        // Act
        // Act
        // 1. First check: should return "User not found"
        string infoBefore = service.GetUserFullInfo(42);
        bool registered = service.RegisterNewUser(validUser.Name, validUser.Age);
        string info = service.GetUserFullInfo(42);  // Assume id=42 after registration

        // Assert - FluentAssertions chain
        infoBefore.Should().Be("User not found"); // Verifies the first null return

        registered.Should().BeTrue("Registration should succeed");

        info.Should()
            .NotBeNullOrEmpty()
            .And.Contain(validUser.Name)
            .And.Contain(validUser.Age.ToString())
            .And.MatchRegex(@"\d+ years old\.$");
    }

    [Theory]
    [InlineAutoMoqData(0)]   // Changed from InlineAutoData
    [InlineAutoMoqData(-5)]  // Changed from InlineAutoData
    public void GetValidatedUser_InvalidId_ThrowsArgumentOutOfRange(
        int invalidId,
        [Frozen] Mock<IUserRepository> mockRepo,
        UserService service)
    {
        // Act & Assert
        service.Invoking(s => s.GetValidatedUser(invalidId))
               .Should().Throw<ArgumentOutOfRangeException>()
               .WithParameterName("id")
               .WithMessage("*positive*");
    }
}
