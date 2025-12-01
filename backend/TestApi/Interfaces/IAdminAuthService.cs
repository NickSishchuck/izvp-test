namespace TestApi.Interfaces
{
    public interface IAdminAuthService
    {
        FluentValidation.Results.ValidationResult Validate(string username, string password);
    }
}
