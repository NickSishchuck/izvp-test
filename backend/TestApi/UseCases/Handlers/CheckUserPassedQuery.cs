using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using TestApi.Interfaces;
using TestApi.UseCases.Queries;

namespace TestApi.UseCases.Handlers
{
    /// <summary>
    /// Handles the <see cref="CheckUserPassedQuery"/> to verify if a user has already completed a specific test.
    /// </summary>
    public class CheckUserPassedQueryHandler(
        ITestRepository repository,
        ILogger<CheckUserPassedQueryHandler> logger)
        : IRequestHandler<CheckUserPassedQuery, Result<Unit, ValidationResult>>
    {
        public async Task<Result<Unit, ValidationResult>> Handle(CheckUserPassedQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Checking if user {Username} has already passed test {TestId}", request.Username, request.TestId);

            var userPassed = await repository.UserAlreadyPassedAsync(request.Username, request.TestId, cancellationToken);

            if (userPassed)
            {
                logger.LogWarning("User {Username} has already passed test {TestId}", request.Username, request.TestId);

                var validationResult = new ValidationResult(new[]
                {
                    new ValidationFailure("Test", "User already passed the test.")
                });

                return Result.Failure<Unit, ValidationResult>(validationResult);
            }
            logger.LogInformation("User {Username} has not passed test {TestId} yet", request.Username, request.TestId);

            return Result.Success<Unit, ValidationResult>(Unit.Value);
        }
    }
}
