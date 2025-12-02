using TestApi.DomainEntities;
using TestApi.Interfaces;

namespace TestApi.Implementations.Repositories
{
    public class TestRepository(IConfiguration configuration, IJsonSerializer jsonSerializer) : ITestRepository
    {
        private readonly string _testResultsPath = configuration["TestResultsPath"]
            ?? throw new InvalidOperationException("Test results not configured");
        private readonly string _mainTestPath = configuration["TestPath"]
            ?? throw new InvalidOperationException("Main test not configured");

        /// <inheritdoc />
        public async Task<TestEntity> LoadMainTest(CancellationToken cancellationToken = default)
        {
            if (!File.Exists(_mainTestPath))
            {
                await File.WriteAllTextAsync(_mainTestPath, "[]", cancellationToken);
                return new TestEntity();
            }

            var json = await File.ReadAllTextAsync(_mainTestPath, cancellationToken);
            return jsonSerializer.FromJson<TestEntity>(json)
                   ?? new TestEntity();
        }

        /// <inheritdoc />
        public async Task SaveMainTestAsync(TestEntity test, CancellationToken cancellationToken = default)
        {
            var json = jsonSerializer.ToJson(test);
            await File.WriteAllTextAsync(_mainTestPath, json, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<List<UserTestResult>> LoadResultsAsync(CancellationToken cancellationToken)
        {
            if (!File.Exists(_testResultsPath))
            {
                await File.WriteAllTextAsync(_testResultsPath, "[]", cancellationToken);
                return new List<UserTestResult>();
            }

            var json = await File.ReadAllTextAsync(_testResultsPath, cancellationToken);
            return jsonSerializer.FromJson<List<UserTestResult>>(json)
                   ?? new List<UserTestResult>();
        }

        /// <inheritdoc />
        public async Task SaveResultsAsync(List<UserTestResult> results, CancellationToken cancellationToken)
        {
            var json = jsonSerializer.ToJson<List<UserTestResult>>(results);
            await File.WriteAllTextAsync(_testResultsPath, json);
        }

        /// <inheritdoc />
        public async Task<bool> UserAlreadyPassedAsync(string userName, Guid testId, CancellationToken cancellationToken)
        {
            var results = await LoadResultsAsync(cancellationToken);
            return results.Any(r => r.UserName == userName && r.TestId == testId);
        }

    }
}
