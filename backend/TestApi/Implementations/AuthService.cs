using FluentValidation.Results;
using TestApi.Interfaces;


namespace TestApi.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly string _adminUsername;
        private readonly string _adminPassword;

        public AuthService(IConfiguration configuration)
        {
            _adminUsername = configuration["AdminSettings:Username"]
                ?? throw new ArgumentNullException("AdminSettings:Username must be configured.");

            _adminPassword = configuration["AdminSettings:Password"]
                ?? throw new ArgumentNullException("AdminSettings:Password must be configured.");
        }

        public ValidationResult Validate(string username, string password)
        {
            var result = new ValidationResult();

            if (username != _adminUsername || password != _adminPassword)
            {
                result.Errors.Add(new ValidationFailure(
                    "Credentials",
                    "Invalid username or password"
                ));
            }
            return result;
        }
    }
}
