using FitTracker.Api.Controllers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using TestApi.DTOs.Requests.TestSubmitRequestAggregate;
using TestApi.Swagger.Examples;
using TestApi.UseCases.Commands;
using TestApi.UseCases.Queries;

namespace TestApi.Controllers
{
    /// <summary>
    /// Represents an controller that provides endpoints for handling HTTP requests related to test operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TestController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Handles HTTP GET requests to retrieve test data.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        [HttpGet]
        public async Task<IActionResult> GetTestAsync(CancellationToken cancellationToken)
        {
            var query = new GetTestQuery();
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result.Value);
        }


        /// <summary>
        /// Handles HTTP POST to submit user`s test.
        /// </summary>
        /// <param name="test">An <see cref="TestSubmitRequest"/>.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        [HttpPost("submit")]
        [SwaggerRequestExample(typeof(TestSubmitRequest), typeof(TestSubmitRequestExample))]
        public async Task<IActionResult> SubmitTestAsync([FromBody] TestSubmitRequest test, CancellationToken cancellationToken)
        {
            var command = new SubmitTestCommand(test, cancellationToken);
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return ValidationProblem(result.Error.ToModelState());
            }

            return Ok(result.Value);
        }
    }
}
