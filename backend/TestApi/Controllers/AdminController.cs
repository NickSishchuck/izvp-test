using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApi.DTOs.Requests;
using TestApi.UseCases.Commands;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] AdminLoginRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new LoginCommand(request.Username, request.Password),
                cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Errors);
        }
    }
}
