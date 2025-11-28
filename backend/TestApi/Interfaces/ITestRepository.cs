using TestApi.DomainEntities;

namespace TestApi.Interfaces
{
    public interface ITestRepository
    {
        /// <summary>
        /// Loads the main test definition used for evaluation.
        /// </summary>
        /// <param name="cancellationToken">The token used to cancel the operation.</param>
        Task<TestEntity> LoadMainTest(CancellationToken cancellationToken);

        /// <summary>
        /// Loads all stored user test results.
        /// </summary>
        /// <param name="cancellationToken">The token used to cancel the operation.</param>
        Task<List<UserTestResult>> LoadResultsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Persists the provided collection of user test results.
        /// </summary>
        /// <param name="results">The results to save.</param>
        /// <param name="cancellationToken">The token used to cancel the operation.</param>
        Task SaveResultsAsync(List<UserTestResult> results, CancellationToken cancellationToken);

        /// <summary>
        /// Checks whether the specified user has already passed the given test.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="testName">The name of the test.</param>
        /// <param name="cancellationToken">The token used to cancel the operation.</param>
        /// <returns>>true</c> if the user has already passed the test; otherwise, >false</c>.</returns>
        Task<bool> UserAlreadyPassedAsync(string userName, string testName, CancellationToken cancellationToken);

    }
}
