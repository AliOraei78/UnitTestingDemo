using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using CoreLogic;
using FluentAssertions;
using Moq;
using Xunit;
using static CoreLogic.Tests.UserServiceTests;

namespace CoreLogic.Tests;

public class RefactoredUserServiceTest
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly IUserValidator _validator;
    private readonly RefactoredUserService _service;

    public RefactoredUserServiceTest()
    {
        _mockRepo = new Mock<IUserRepository>();
        _validator = new DefaultUserValidator();
        _service = new RefactoredUserService(_mockRepo.Object, _validator);
    }

    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] objects)
            : base(new AutoMoqDataAttribute(), objects)
        {
        }
    }

    [Theory]
    [AutoMoqData]
    public void RegisterNewUser_InvalidName_UsesValidator_LogsError_DoesNotSave(
    [Frozen] Mock<IUserRepository> mockRepo,
    [Frozen] Mock<IUserValidator> mockValidator,
    RefactoredUserService service)
    {
        mockValidator
            .Setup(v => v.IsValidRegistration(" ", It.IsAny<int>(), out It.Ref<string>.IsAny))
            .Returns(false);

        var success = service.RegisterNewUser(" ", 25);

        success.Should().BeFalse();
        mockRepo.Verify(r => r.SaveUser(It.IsAny<User>()), Times.Never());
    }

    [Theory]
    [AutoMoqData]
    public void RegisterNewUser_ValidData_UsesValidator_SavesUser_ReturnsTrue(
    [Frozen] Mock<IUserRepository> mockRepo,
    [Frozen] Mock<IUserValidator> mockValidator,
    RefactoredUserService service)
    {
        // Arrange
        string name = "John Doe";
        int age = 25;
        string noError = string.Empty;

        // Setup validator to return true for valid input
        mockValidator
            .Setup(v => v.IsValidRegistration(name, age, out noError))
            .Returns(true);

        // Setup repository to return true when saving
        mockRepo
            .Setup(r => r.SaveUser(It.IsAny<User>()))
            .Returns(true);

        // Act
        var success = service.RegisterNewUser(name, age);

        // Assert
        success.Should().BeTrue();

        // Verify that SaveUser was called exactly once with a user object having the correct data
        mockRepo.Verify(r => r.SaveUser(It.Is<User>(u => u.Name == name && u.Age == age)), Times.Once());
    }
}