using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using TestApi.DTOs.Requests.TestSubmitRequestAggregate;
using TestApi.DTOs.Responses;

namespace TestApi.UseCases.Commands
{
    /// <summary>
    /// Represents a command to submit a completed test for evaluation.
    /// </summary>
    /// <param name="Test">The <see cref="TestSubmitRequest"/>.</param>
    [ExcludeFromCodeCoverage]
    public record SubmitTestCommand(TestSubmitRequest Test) : IRequest<Result<TestResultResponse, ValidationResult>>;
}