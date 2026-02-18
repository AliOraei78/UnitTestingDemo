public interface IUserValidator
{
    bool IsValidRegistration(string name, int age, out string errorMessage);
}