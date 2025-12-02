using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using TestApi.DTOs.Responses;
using TestApi.DTOs.Requests;
using TestApi.DTOs.Responses.TestApi.DTOs.Responses;
using System.Diagnostics.CodeAnalysis;

namespace TestApi.UseCases.Commands
{
    [ExcludeFromCodeCoverage]
    public record LoginCommand(AdminLoginRequest Request)
        : IRequest<Result<AdminLoginResponse, ValidationResult>>;
}
