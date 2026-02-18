using CoreLogic.Logging;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace CoreLogic;

public class RefactoredUserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger _logger;
    private readonly IUserValidator _validator;
    // Using Serilog.ILogger (not Microsoft ILogger)

    public RefactoredUserService(
        IUserRepository repository,
        IUserValidator validator,
        ILogger logger = null)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _logger = logger ?? Log.ForContext<UserService>();
    }

    public bool RegisterNewUser(string name, int age)
    {
        if (!_validator.IsValidRegistration(name, age, out var error))
        {
            _logger.Error("Invalid registration: {Error}", error);
            return false;
        }

        var user = new User { Name = name, Age = age };
        var saved = _repository.SaveUser(user);

        if (!saved)
            _logger.Error("Failed to persist user: {Name}", name);

        return saved;
    }
}
