using FluentValidation;
using TestApi.DTOs.Requests;

namespace TestApi.Validators
{
    /// <summary>
    /// Basic validation rules for the incoming login request DTO.
    /// This validator ensures only that fields are not empty
    /// and meet minimal formatting requirements.
    /// </summary>
    public class AdminLoginRequestValidator : AbstractValidator<AdminLoginRequest>
    {
        public AdminLoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}