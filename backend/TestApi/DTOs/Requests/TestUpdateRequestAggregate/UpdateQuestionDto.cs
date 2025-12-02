
using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Requests.TestUpdateRequestAggregate
{
    [ExcludeFromCodeCoverage]
    public class UpdateQuestionDto
    {
        /// <summary>
        /// Unique identifier for the dto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Text content.
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// Type of the question.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Score value assigned to the question.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Correct text answer for the question.
        /// </summary>
        public string? CorrectTextAnswer { get; set; }

        /// <summary>
        /// Collection of available answer options.
        /// </summary>
        public List<UpdateAnswerOptionDto> Options { get; set; } = new();
    }
}