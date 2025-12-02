using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestApi.Filters
{
    public class AdminAuthorizationFilter(IConfiguration configuration) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("X-Admin-Token", out var token))
            {
                var problemDetails = new ValidationProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = "One or more validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Token is required",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Token", new[] { "Token is required" } }
                    }
                };

                context.Result = new BadRequestObjectResult(problemDetails);
                return;
            }

            var validToken = configuration["AdminSettings:Token"];
            if (token != validToken)
            {
                var problemDetails = new ValidationProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = "One or more validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Invalid token",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Token", new[] { "Invalid token" } }
                    }
                };

                context.Result = new BadRequestObjectResult(problemDetails);
                return;
            }
        }
    }
}
