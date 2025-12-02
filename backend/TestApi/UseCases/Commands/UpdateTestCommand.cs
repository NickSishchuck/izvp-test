using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using MediatR;
using TestApi.DomainEntities;
using TestApi.DTOs.Requests.TestUpdateRequestAggregate;

namespace TestApi.UseCases.Commands
{
    [ExcludeFromCodeCoverage]
    public record UpdateTestCommand(UpdateTestRequest Request) : IRequest<Result<TestEntity>>;
}
