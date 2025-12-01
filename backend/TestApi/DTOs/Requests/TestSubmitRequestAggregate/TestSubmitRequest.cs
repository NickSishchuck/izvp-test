using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Requests.TestSubmitRequestAggregate
{
    /// <summary>
    /// Represents a test submission containing the test title, user information, and user answers.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TestSubmitRequest
    {
        /// <summary>
        /// Unique id for test
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        /// The title of the test being submitted.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The name of the user who is submitting the test.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The collection of answers provided by the user for this test.
        /// </summary>
        public List<AnswerDto> Answers { get; set; }
    }
}
