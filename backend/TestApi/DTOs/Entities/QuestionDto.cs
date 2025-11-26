using System.Diagnostics.CodeAnalysis;
using TestApi.DomainEntities;

namespace TestApi.DTOs.Entities
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// DTO representing a question.
    /// </summary>
    public class QuestionDto
    {
        /// <summary>
        /// The unique identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Text content associated with this instance.
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// Score value assigned to the question.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The type of the question.
        /// </summary>
        public string Type { get; set; } = "";

        /// <summary>
        /// The list of <see cref="AnswerOptionDto"/> associated with this question.
        /// </summary>
        public List<AnswerOptionDto> Options { get; set; } = new();
    }
}
