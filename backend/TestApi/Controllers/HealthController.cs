using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApi.Commands;

namespace TestApi.Controllers
{
    /// <summary>
    /// Provides an API endpoint for reporting the health status of the application.
    /// </summary>
    /// <param name="mediator">The mediator instance used to send commands and queries.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Returns the health status of the application as an HTTP response.
        /// </summary>
        /// <param name="cancellation">The cancellation token to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing a JSON object with the application's health status. The response includes a property named <c>status</c> with the value "Healthy".</returns>
        [HttpGet]
        public async Task<IActionResult> GetHealth(CancellationToken cancellation)
        {
            var command = new GetHealthCommand();
            var result = await mediator.Send(command, cancellation);

            if (result.IsSuccess)
                return Ok(result.Value);

            return Problem(statusCode: StatusCodes.Status500InternalServerError, title: result.Error);
        }
    }
}
