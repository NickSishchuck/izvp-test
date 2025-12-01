using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TestApi.DTOs.Responses;
using TestApi.DTOs.Responses.TestApi.DTOs.Responses;
using TestApi.Interfaces;
using TestApi.UseCases.Commands;

namespace TestApi.UseCases.Handlers
{
    public class LoginCommandHandler : IRequestHandler<
        LoginCommand,
        Result<AdminLoginResponse, ValidationResult>>
    {
        private readonly IValidator<LoginCommand> _validator;
        private readonly IAdminAuthService _authService; 

        public LoginCommandHandler(
            IValidator<LoginCommand> validator,
            IAdminAuthService authService) 
        {
            _validator = validator;
            _authService = authService;
        }

        public async Task<Result<AdminLoginResponse, ValidationResult>> Handle(
         LoginCommand request,
        CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return Result.Failure<AdminLoginResponse, ValidationResult>(validation);

            var authValidation = _authService.Validate(request.Username, request.Password);

            if (!authValidation.IsValid)
                return Result.Failure<AdminLoginResponse, ValidationResult>(authValidation);

            string token = "generated-admin-jwt-token";

            return Result.Success<AdminLoginResponse, ValidationResult>(
                new AdminLoginResponse(token));
        }
    }
}