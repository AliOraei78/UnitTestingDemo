using CoreLogic;
using CoreLogic.Logging;
using FluentAssertions;
using Moq;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.InMemory;
using Xunit;

namespace CoreLogic.Tests;

public class UserServiceLoggingTests
{
    private readonly UserService _service;
    private readonly Mock<IUserRepository> _mockRepo;

    public UserServiceLoggingTests()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Environment", "UnitTesting") // Add property for easier filtering in Seq
            .WriteTo.InMemory()
            .WriteTo.Seq("http://localhost:5341") // Send test logs to Seq
            .WriteTo.File("logs/test-log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


        _mockRepo = new Mock<IUserRepository>();
        _service = new UserService(_mockRepo.Object);
    }

    [Fact]
    public void GetUserFullInfo_UserNotFound_LogsWarning()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetUserById(999)).Returns((User?)null);

        // Act
        _ = _service.GetUserFullInfo(999);

        // Assert
        // Use InMemorySink.Instance.LogEvents to access the logs
        var logs = InMemorySink.Instance.LogEvents;

        logs.Should().ContainSingle(e =>
            e.Level == LogEventLevel.Warning &&
            e.MessageTemplate.Text.Contains("User not found") &&
            e.Properties.ContainsKey("UserId")
        );
    }

    [Fact]
    public void RegisterNewUser_InvalidAge_LogsError()
    {
        // Act
        var result = _service.RegisterNewUser("Teen", 16);

        // Assert
        result.Should().BeFalse();

        var logs = InMemorySink.Instance.LogEvents;
        logs.Should().Contain(e =>
            e.Level == LogEventLevel.Error &&
            e.MessageTemplate.Text.Contains("Invalid registration attempt")
        );
    }
}