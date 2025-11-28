using CSharpFunctionalExtensions;
using MediatR;
using TestApi.DTOs.Responses;
using TestApi.UseCases.Queries;

namespace TestApi.UseCases.Handlers
{
    /// <summary>
    /// Handles the processing of a health check command and returns the current health status of the application.
    /// </summary>
    public class GetHealthQueryHandler() : IRequestHandler<GetHealthQuery, Result<HealthStatusResponse>>
    {
        /// <summary>
        /// Handles the specified health check command and returns the current health status of the application.
        /// </summary>
        /// <param name="request">The health check command <see cref="GetHealthQuery"/> containing the parameters for the health status request.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Result{HealthStatusResponse}"/> containing the health status of the application.</returns>
        public async Task<Result<HealthStatusResponse>> Handle(GetHealthQuery request, CancellationToken cancellationToken)
        {
            var response = new HealthStatusResponse("Healthy", DateTime.UtcNow);

            return Result.Success(response);
        }
    }
}
