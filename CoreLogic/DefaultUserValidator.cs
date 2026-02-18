public class DefaultUserValidator : IUserValidator
{
    public bool IsValidRegistration(string name, int age, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(name))
        {
            errorMessage = "Name cannot be empty.";
            return false;
        }

        if (age < 18)
        {
            errorMessage = "User must be at least 18 years old.";
            return false;
        }

        return true;
    }
}