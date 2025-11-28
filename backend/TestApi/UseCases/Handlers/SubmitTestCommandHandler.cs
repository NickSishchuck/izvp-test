using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TestApi.DomainEntities;
using TestApi.DTOs.Requests.TestSubmitRequestAggregate;
using TestApi.DTOs.Responses;
using TestApi.Interfaces;
using TestApi.UseCases.Commands;

namespace TestApi.UseCases.Handlers
{
    /// <summary>
    /// Handles test submission commands: validates input, evaluates the test, and stores the result.
    /// </summary>
    public class SubmitTestCommandHandler(
        IValidator<TestSubmitRequest> validator,
        ITestRepository testRepository,
        ITestEvaluationService evaluationService,
        ILogger<SubmitTestCommandHandler> logger)
        : IRequestHandler<SubmitTestCommand, Result<TestResultResponse, ValidationResult>>
    {
        /// <summary>
        /// Processes the incoming test submission and returns either a test result or validation errors.
        /// </summary>
        public async Task<Result<TestResultResponse, ValidationResult>> Handle(
            SubmitTestCommand request,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Start handling SubmitTestCommand for user {UserName}, title {Title}",
                request.Test.UserName,
                request.Test.Title);

            // Validate incoming DTO using FluentValidation
            var validationResult = await validator.ValidateAsync(request.Test, cancellationToken);
            if (!validationResult.IsValid)
            {
                logger.LogWarning(
                    "Validation failed for SubmitTestCommand. Errors: {Errors}",
                    string.Join("; ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));

                return Result.Failure<TestResultResponse, ValidationResult>(validationResult);
            }

            var testDto = request.Test;

            // Ensure the user has not already passed this test
            bool alreadyPassed = await testRepository.UserAlreadyPassedAsync(
                testDto.UserName,
                testDto.Title,
                cancellationToken);

            if (alreadyPassed)
            {
                logger.LogWarning(
                    "User {UserName} has already passed test {Title}",
                    testDto.UserName,
                    testDto.Title);

                var vr = new ValidationResult(new[]
                {
                    new ValidationFailure("TestName", "User already passed the test")
                });

                return Result.Failure<TestResultResponse, ValidationResult>(vr);
            }

            // Evaluate test answers and calculate the result
            var result = await evaluationService.EvaluateAsync(testDto, cancellationToken);

            logger.LogDebug(
                "Evaluation completed. CorrectAnswers: {CorrectAnswers}, TotalQuestions: {TotalQuestions}, Score: {Score}, TotalScore: {TotalScore}",
                result.CorrectAnswers,
                result.TotalQuestions,
                result.Score,
                result.TotalScore);

            // Load existing results and append the new one
            var allResults = await testRepository.LoadResultsAsync(cancellationToken);

            logger.LogDebug("Loaded {Count} existing test results", allResults.Count);

            var newResult = new UserTestResult
            {
                UserName = testDto.UserName,
                TestName = testDto.Title,
                Score = result.Score,
                PassedAt = DateTime.UtcNow
            };

            logger.LogDebug(
                "Appending new test result for user {UserName}, title {Title}, score {Score}",
                newResult.UserName,
                newResult.TestName,
                newResult.Score);

            allResults.Add(newResult);

            await testRepository.SaveResultsAsync(allResults, CancellationToken.None);

            logger.LogInformation(
                "SubmitTestCommand handled successfully for user {UserName}, title {Title}",
                testDto.UserName,
                testDto.Title);

            return Result.Success<TestResultResponse, ValidationResult>(result);
        }
    }
}
