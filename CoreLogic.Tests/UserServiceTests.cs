using CoreLogic;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreLogic.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _service = new UserService(_mockRepo.Object);
    }

    [Fact]
    public void GetUserFullInfo_ExistingUser_ReturnsFormattedString()
    {
        // Arrange
        var user = new User { Id = 42, Name = "Alice", Age = 30 };
        _mockRepo.Setup(r => r.GetUserById(42))
                 .Returns(user);

        // Act
        var result = _service.GetUserFullInfo(42);

        // Assert
        result.Should().Be("Alice is 30 years old.");
        _mockRepo.Verify(r => r.GetUserById(42), Times.Once());
    }

    [Fact]
    public void GetUserFullInfo_NonExistingUser_ReturnsNotFoundMessage()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetUserById(It.IsAny<int>()))
                 .Returns((User?)null);

        // Act
        var result = _service.GetUserFullInfo(999);

        // Assert
        result.Should().Be("User not found");
        _mockRepo.Verify(r => r.GetUserById(999), Times.Once());
    }

    [Fact]
    public void RegisterNewUser_ValidData_CallsSaveAndReturnsTrue()
    {
        // Arrange
        _mockRepo.Setup(r => r.SaveUser(It.IsAny<User>()))
                 .Returns(true);

        // Act
        var success = _service.RegisterNewUser("Bob", 25);

        // Assert
        success.Should().BeTrue();
        _mockRepo.Verify(r => r.SaveUser(It.Is<User>(u =>
            u.Name == "Bob" && u.Age == 25)), Times.Once());
    }

    [Fact]
    public void RegisterNewUser_UnderAge_ReturnsFalse_WithoutCallingRepo()
    {
        // Act
        var success = _service.RegisterNewUser("Charlie", 16);

        // Assert
        success.Should().BeFalse();
        _mockRepo.Verify(r => r.SaveUser(It.IsAny<User>()), Times.Never());
    }

    [Fact]
    public void GetUserFullInfo_RepoThrowsException_PropagatesToCaller()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetUserById(It.IsAny<int>()))
                 .Throws(new InvalidOperationException("Database unavailable"));

        // Act
        Action act = () => _service.GetUserFullInfo(1);

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Database unavailable");
    }

    [Fact]
    public void RegisterNewUser_WithCallback_TracksSavedUser()
    {
        // Arrange
        User? savedUser = null;

        _mockRepo.Setup(r => r.SaveUser(It.IsAny<User>()))
                 .Callback<User>(u => savedUser = u)
                 .Returns(true);

        // Act
        _service.RegisterNewUser("David", 40);

        // Assert
        savedUser.Should().NotBeNull();
        savedUser!.Name.Should().Be("David");
        savedUser.Age.Should().Be(40);
    }
}