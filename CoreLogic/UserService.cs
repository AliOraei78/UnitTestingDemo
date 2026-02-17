using CoreLogic;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public string GetUserFullInfo(int id)
    {
        var user = _repository.GetUserById(id);
        if (user == null)
            return "User not found";

        return $"{user.Name} is {user.Age} years old.";
    }

    public bool RegisterNewUser(string name, int age)
    {
        if (string.IsNullOrWhiteSpace(name) || age < 18)
            return false;

        var user = new User { Name = name, Age = age };
        return _repository.SaveUser(user);
    }
}