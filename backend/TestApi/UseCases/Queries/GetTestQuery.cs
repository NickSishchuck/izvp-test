using CSharpFunctionalExtensions;
using MediatR;
using TestApi.DTOs.Responses.TestResponseAggregate;

namespace TestApi.UseCases.Queries
{
    /// <summary>
    /// Query to get a test.
    /// </summary>
    public record GetTestQuery() : IRequest<Result<TestResponse>>;
}
