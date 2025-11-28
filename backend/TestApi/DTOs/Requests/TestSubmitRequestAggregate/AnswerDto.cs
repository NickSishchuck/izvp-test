using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Requests.TestSubmitRequestAggregate
{
    /// <summary>
    /// Represents a single user answer submitted for a specific test question.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AnswerDto
    {
        /// <summary>
        /// The identifier of the question this answer belongs to.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The collection of selected option identifiers
        /// for choice-based questions (single- or multiple-choice).
        /// </summary>
        public List<int> SelectedOptionIds { get; set; }

        /// <summary>
        /// The free-form text answer for text-based questions.
        /// </summary>
        public string? TextAnswer { get; set; }
    }
}
