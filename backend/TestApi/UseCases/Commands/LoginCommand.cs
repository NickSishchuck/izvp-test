using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using TestApi.DTOs.Responses;
using TestApi.DTOs.Requests;
using TestApi.DTOs.Responses.TestApi.DTOs.Responses;

namespace TestApi.UseCases.Commands
{
    public record LoginCommand(AdminLoginRequest Request)
        : IRequest<Result<AdminLoginResponse, ValidationResult>>;
}
