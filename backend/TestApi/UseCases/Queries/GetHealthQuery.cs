using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using MediatR;
using TestApi.DTOs.Responses;

namespace TestApi.UseCases.Queries
{
    /// <summary>
    /// Represents a request to retrieve the current health status of the application or service.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public record GetHealthQuery() : IRequest<Result<HealthStatusResponse>>;
}
