namespace TestApi.Interfaces
{
    public interface IAuthService
    {
        FluentValidation.Results.ValidationResult Validate(string username, string password);
    }
}
