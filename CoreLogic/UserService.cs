using CoreLogic.Logging;
using Serilog;

namespace CoreLogic;

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger _logger;
    // Using Serilog.ILogger (not Microsoft ILogger)

    public UserService(IUserRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = Log.ForContext<UserService>();
        // Context-aware logger
    }

    public string GetUserFullInfo(int id)
    {
        _logger.Debug("Fetching user with ID {UserId}", id);

        var user = _repository.GetUserById(id);
        if (user == null)
        {
            _logger.Warning("User not found for ID {UserId}", id);
            return "User not found";
        }

        _logger.Information(
            "User retrieved: {UserName}, Age: {UserAge}",
            user.Name,
            user.Age
        );

        return $"{user.Name} is {user.Age} years old.";
    }

    public bool RegisterNewUser(string name, int age)
    {
        if (string.IsNullOrWhiteSpace(name) || age < 18)
        {
            _logger.Error(
                "Invalid registration attempt: Name={Name}, Age={Age}",
                name,
                age
            );
            return false;
        }

        var user = new User { Name = name, Age = age };
        var saved = _repository.SaveUser(user);

        if (saved)
            _logger.Information("User registered successfully: {UserName}", name);
        else
            _logger.Error("Failed to save user: {UserName}", name);

        return saved;
    }
}
