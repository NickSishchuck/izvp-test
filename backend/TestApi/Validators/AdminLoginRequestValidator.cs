using FluentValidation;
using TestApi.DTOs.Requests;

namespace TestApi.Validators
{
    public class AdminLoginRequestValidator : AbstractValidator<AdminLoginRequest>
    {
        public AdminLoginRequestValidator()
        {
            // Validation rules for the Username property
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required")
                .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters");

            // Validation rules for the Password property
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                // You can add more password-specific rules here (e.g., MinimumLength(6))
                ;
        }
    }
}
