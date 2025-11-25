using CSharpFunctionalExtensions;
using MediatR;
using TestApi.DTOs.Responses;

namespace TestApi.UseCases.Queries
{
    /// <summary>
    /// Query to get a test.
    /// </summary>
    public record GetTestQuery() : IRequest<Result<TestResponseDto>>;
}
