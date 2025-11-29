using TestApi.Results;
using MediatR;

namespace TestApi.UseCases.Commands
{
    public record LoginCommand(string Username, string Password)
          : IRequest<Result<string>>;
}
