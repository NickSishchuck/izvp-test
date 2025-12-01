using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Responses.TestResponseAggregate
{
    /// <summary>
    /// DTO representing a test response.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TestResponse
    {
        /// <summary>
        /// Unique id for test
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The title of the test.
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// The list of <see cref="QuestionDto"/> in the test.
        /// </summary>
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
