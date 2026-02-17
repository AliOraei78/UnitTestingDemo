namespace CoreLogic;

public interface IUserRepository
{
    User? GetUserById(int id);
    bool SaveUser(User user);
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}