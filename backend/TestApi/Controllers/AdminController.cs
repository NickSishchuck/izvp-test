using FitTracker.Api.Controllers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApi.DTOs.Requests;
using TestApi.UseCases.Commands;
using TestApi.DTOs.Requests;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController(IMediator mediator) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] AdminLoginRequest request,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(
                new LoginCommand(request.Username, request.Password),
                cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : ValidationProblem(result.Error.ToModelState());
        }
    }
}
