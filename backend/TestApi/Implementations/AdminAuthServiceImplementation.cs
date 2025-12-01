using FluentValidation.Results;
using TestApi.Interfaces;


namespace TestApi.Implementations
{
    public class AdminAuthServiceImplementation : IAdminAuthService
    {
        private readonly string _adminUsername;
        private readonly string _adminPassword;

        public AdminAuthServiceImplementation(IConfiguration configuration)
        {
            _adminUsername = configuration["AdminSettings:Username"]
                ?? throw new ArgumentNullException("AdminSettings:Username must be configured.");

            _adminPassword = configuration["AdminSettings:Password"]
                ?? throw new ArgumentNullException("AdminSettings:Password must be configured.");
        }

        public ValidationResult Validate(string username, string password)
        {
            var result = new ValidationResult();

            if (username != _adminUsername)
                result.Errors.Add(new ValidationFailure("Username", "Invalid username"));

            if (password != _adminPassword)
                result.Errors.Add(new ValidationFailure("Password", "Invalid password"));

            return result;
        }
    }
}
