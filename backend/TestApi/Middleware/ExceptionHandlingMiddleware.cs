using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Middleware
{
    /// <summary>
    /// Middleware for handling unhandled exceptions globally.
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        /// <summary>
        /// Invokes the middleware to handle exceptions.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError("An unhandled exception occurred: {Message}", ex.Message);

                // Create a ProblemDetails response
                var problem = new ProblemDetails
                {
                    // Link to more info about 500 errors
                    Type = "https://httpstatuses.com/500",

                    // Short, human-readable summary
                    Title = "Internal Server Error", 

                    // HTTP status code
                    Status = (int)HttpStatusCode.InternalServerError,

                    // Detailed error message
                    Detail = "An unexpected error occurred. Please contact support.",

                    // Unique identifier for this occurrence
                    Instance = context.TraceIdentifier
                };

                context.Response.StatusCode = problem.Status ?? (int)HttpStatusCode.InternalServerError;

                // Standard content type for ProblemDetails
                context.Response.ContentType = "application/problem+json";

                // Serialize ProblemDetails to JSON
                var json = JsonSerializer.Serialize(problem);

                // Write JSON response
                await context.Response.WriteAsync(json);
            }
        }
    }
}
