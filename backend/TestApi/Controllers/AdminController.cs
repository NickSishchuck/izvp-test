using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using TestApi.DTOs.Requests.TestUpdateRequestAggregate;
using TestApi.Swagger.Examples;
using TestApi.UseCases.Commands;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController(IMediator mediator) : ControllerBase
    {
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
