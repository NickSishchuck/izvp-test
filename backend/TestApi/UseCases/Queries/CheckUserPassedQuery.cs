using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;

namespace TestApi.UseCases.Queries
{
    /// <summary>
    /// Represents a query to check if a user has already passed a specific test.
    /// </summary>
    /// <param name="Username">The username of the user to check.</param>
    /// <param name="TestId">The unique identifier of the test.</param>
    [ExcludeFromCodeCoverage]
    public record CheckUserPassedQuery(string Username, Guid TestId) : IRequest<Result<Unit, ValidationResult>>;
}
