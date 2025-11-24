using FluentValidation.Results;
using CSharpFunctionalExtensions;
using TestApi.DTOs.Responses;
using MediatR;

namespace TestApi.UseCases.Commands
{
    /// <summary>
    /// Represents a request to retrieve the current health status of the application or service.
    /// </summary>
    public record GetHealthCommand() : IRequest<Result<HealthStatusResponse>>;
}
