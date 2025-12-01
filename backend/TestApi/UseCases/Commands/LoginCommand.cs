using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using TestApi.DTOs.Responses;
using TestApi.DTOs.Responses.TestApi.DTOs.Responses;

namespace TestApi.UseCases.Commands
{
    public record LoginCommand(string Username, string Password)
        : IRequest<Result<AdminLoginResponse, ValidationResult>>;
}
