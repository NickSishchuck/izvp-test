using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApi.DomainEntities;
using TestApi.DTOs.Responses;
using TestApi.UseCases.Queries;

namespace TestApi.Controllers
{
    /// <summary>
    /// Represents an controller that provides endpoints for handling HTTP requests related to test opeations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TestController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Handles HTTP GET requests to retrieve test data.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing a <see cref="TestResponseDto"/> </returns>
        [HttpGet]
        public async Task<IActionResult> GetTestAsync(CancellationToken cancellationToken)
        {
            var query = new GetTestQuery();
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result.Value);
        }
    }
}
