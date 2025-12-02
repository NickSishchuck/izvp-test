using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using TestApi.DTOs.Requests; // Для AdminLoginRequest
using TestApi.DTOs.Responses;
using TestApi.DTOs.Responses.TestApi.DTOs.Responses;
using TestApi.Interfaces;
using TestApi.UseCases.Commands;

namespace TestApi.UseCases.Handlers
{
    /// <summary>
    /// Handles the admin login command by validating input and verifying credentials.
    /// </summary>
    public class LoginCommandHandler(
        // Валідатор тепер працює з AdminLoginRequest
        IValidator<AdminLoginRequest> validator,
        // Інтерфейс сервісу аутентифікації
        IAuthService authService,
        ILogger<LoginCommandHandler> logger)
        : IRequestHandler<LoginCommand, Result<AdminLoginResponse, ValidationResult>>
    {
        /// <summary>
        /// Processes the admin login request and returns a JWT token upon successful authentication.
        /// </summary>
        public async Task<Result<AdminLoginResponse, ValidationResult>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            // Отримуємо DTO з команди (припускаємо, що LoginCommand має властивість Request типу AdminLoginRequest)
            var adminRequest = request.Request;

            logger.LogInformation("Start handling LoginCommand for user {Username}", adminRequest.Username);

            // 1. Validate incoming DTO using FluentValidation
            // Валідуємо AdminLoginRequest
            var validation = await validator.ValidateAsync(adminRequest, cancellationToken);

            if (!validation.IsValid)
            {
                logger.LogWarning("Basic validation failed for LoginCommand. User: {Username}. Errors: {Errors}",
                    adminRequest.Username, string.Join("; ", validation.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
                return Result.Failure<AdminLoginResponse, ValidationResult>(validation);
            }

            // 2. Validate credentials against configuration (AdminAuthService)
            var authValidation = authService.Validate(adminRequest.Username, adminRequest.Password);

            if (!authValidation.IsValid)
            {
                logger.LogWarning("Credential validation failed for LoginCommand. User: {Username}.", adminRequest.Username);
                return Result.Failure<AdminLoginResponse, ValidationResult>(authValidation);
            }

            // 3. Issue Token
            string token = "generated-admin-jwt-token";

            logger.LogInformation("LoginCommand handled successfully. Token issued for user {Username}", adminRequest.Username);

            return Result.Success<AdminLoginResponse, ValidationResult>(
                new AdminLoginResponse(token));
        }
    }
}