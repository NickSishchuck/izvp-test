using TestApi.DomainEntities;
using TestApi.Interfaces;

namespace TestApi.Implementations.Repositories
{
    public class TestRepository(IConfiguration configuration, IJsonSerializer jsonSerializer) : ITestRepository
    {
        private readonly string _testResultsPath = configuration["TestResultsPath"];
        private readonly string _mainTestPath = configuration["TestPath"];

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

        public async Task SaveResultsAsync(List<UserTestResult> results, CancellationToken cancellationToken)
        {
            var json = jsonSerializer.ToJson<List<UserTestResult>>(results);
            await File.WriteAllTextAsync(_testResultsPath, json);
        }

        public async Task<bool> UserAlreadyPassedAsync(string userName, string testName, CancellationToken cancellationToken)
        {
            var results = await LoadResultsAsync(cancellationToken);
            return results.Any(r => r.UserName == userName && r.TestName == testName);
        }

    }
}
