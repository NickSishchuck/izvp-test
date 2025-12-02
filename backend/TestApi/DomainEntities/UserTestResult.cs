using System.Diagnostics.CodeAnalysis;

namespace TestApi.DomainEntities
{
    /// <summary>
    /// Represents the result of a single test completed by a user.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public record UserTestResult
    {
        /// <summary>
        /// The name of the user who completed the test.
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Unique id for test
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        /// The name of the test that was completed.
        /// </summary>
        public required string TestName { get; set; }

        /// <summary>
        /// The score the user achieved for this test.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The score the user could achieve for this test.
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        /// The UTC date and time when the test was completed.
        /// </summary>
        public DateTime PassedAt { get; set; }
    }
}
