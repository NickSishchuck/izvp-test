using TestApi.DTOs.Requests.TestSubmitRequestAggregate;
using TestApi.DTOs.Responses;

namespace TestApi.Interfaces
{
    public interface ITestEvaluationService
    {
        /// <summary>
        /// Evaluates the submitted test and returns the calculated result.
        /// </summary>
        /// <param name="request">The test submission containing user answers.</param>
        /// <param name="cancellationToken">The token used to cancel the operation.</param>
        /// <returns>The calculated <see cref="TestResultResponse"/>.</returns>
        Task<TestResultResponse> EvaluateAsync(TestSubmitRequest request, CancellationToken cancellationToken);
    }
}