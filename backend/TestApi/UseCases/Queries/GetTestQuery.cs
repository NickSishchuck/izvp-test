using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using MediatR;
using TestApi.DTOs.Responses.TestResponseAggregate;

namespace TestApi.UseCases.Queries
{
    /// <summary>
    /// Query to get a test.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public record GetTestQuery() : IRequest<Result<TestResponse>>;
}
