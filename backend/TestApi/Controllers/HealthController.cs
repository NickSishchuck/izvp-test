using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    /// <summary>
    /// Provides an API endpoint for reporting the health status of the application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Returns the health status of the application as an HTTP response.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing a JSON object with the application's health status. The response
        /// includes a property named <c>status</c> with the value "Healthy".</returns>
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new { status = "Healthy" });
        }
    }
}
