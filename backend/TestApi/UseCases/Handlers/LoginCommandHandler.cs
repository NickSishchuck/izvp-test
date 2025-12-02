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
    /// Handles the <see cref="LoginCommand"/> by validating input and verifying credentials.
    /// </summary>
    public class LoginCommandHandler(
        IValidator<AdminLoginRequest> validator,
        IAuthService authService,
        ILogger<LoginCommandHandler> logger,
        IConfiguration configuration)
        : IRequestHandler<LoginCommand, Result<AdminLoginResponse, ValidationResult>>
    {
        /// <summary>
        /// Processes the admin login request and returns a JWT token upon successful authentication.
        /// </summary>
        public async Task<Result<AdminLoginResponse, ValidationResult>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var adminRequest = request.Request;

            logger.LogInformation("Start handling LoginCommand for user {Username}", adminRequest.Username);

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
            string token = configuration["AdminSettings:Token"]
                ?? throw new InvalidOperationException("Admin token is not configured");

            logger.LogInformation("LoginCommand handled successfully. Token issued for user {Username}", adminRequest.Username);

            return Result.Success<AdminLoginResponse, ValidationResult>(
                new AdminLoginResponse(token));
        }
    }
}