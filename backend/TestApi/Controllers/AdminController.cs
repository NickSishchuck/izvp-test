using FitTracker.Api.Controllers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApi.DTOs.Requests;
using TestApi.UseCases.Commands;
using TestApi.DTOs.Requests;
using Swashbuckle.AspNetCore.Filters;
using TestApi.DTOs.Requests.TestUpdateRequestAggregate;
using TestApi.Swagger.Examples;
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
                new LoginCommand(request),
                cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : ValidationProblem(result.Error.ToModelState());
        }

        [HttpPut("change")]
        [SwaggerRequestExample(typeof(UpdateTestRequest), typeof(UpdateTestRequestExample))]
        public async Task<IActionResult> ChangeTestAsync(UpdateTestRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateTestCommand(request);
            var result = await mediator.Send(command);

            return Ok(result.Value);
        }
    }
}
