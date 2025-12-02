using TestApi.DomainEntities;
using TestApi.Interfaces;

namespace TestApi.Implementations.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly string _testResultsPath;
        private readonly string _mainTestPath;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILogger<TestRepository> _logger;

        public TestRepository(
            IConfiguration configuration,
            IJsonSerializer jsonSerializer,
            ILogger<TestRepository> logger)
        {
            _jsonSerializer = jsonSerializer;
            _logger = logger;

            _testResultsPath = configuration["TestResultsPath"]
                ?? throw new InvalidOperationException("Test results path not configured");
            _mainTestPath = configuration["TestPath"]
                ?? throw new InvalidOperationException("Main test path not configured");

            EnsureFilesExist();
        }

        private void EnsureFilesExist()
        {
            EnsureFileExists(_mainTestPath, _jsonSerializer.ToJson(new TestEntity
            {
                Id = Guid.Empty,
                Title = "Default Test",
                Questions = new List<Question>()
            }));

            EnsureFileExists(_testResultsPath, "[]");
        }

        private void EnsureFileExists(string filePath, string defaultContent)
        {
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                _logger.LogInformation("Created directory: {Directory}", directory);
            }

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, defaultContent);
                _logger.LogInformation("Created default file: {FilePath}", filePath);
            }
        }


        /// <inheritdoc />
        public async Task<TestEntity> LoadMainTest(CancellationToken cancellationToken = default)
        {
            if (!File.Exists(_mainTestPath))
            {
                await File.WriteAllTextAsync(_mainTestPath, "[]", cancellationToken);
                return new TestEntity();
            }

            var json = await File.ReadAllTextAsync(_mainTestPath, cancellationToken);
            return _jsonSerializer.FromJson<TestEntity>(json)
                   ?? new TestEntity();
        }

        /// <inheritdoc />
        public async Task SaveMainTestAsync(TestEntity test, CancellationToken cancellationToken = default)
        {
            var json = _jsonSerializer.ToJson(test);
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
            return _jsonSerializer.FromJson<List<UserTestResult>>(json)
                   ?? new List<UserTestResult>();
        }

        /// <inheritdoc />
        public async Task SaveResultsAsync(List<UserTestResult> results, CancellationToken cancellationToken)
        {
            var json = _jsonSerializer.ToJson<List<UserTestResult>>(results);
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
