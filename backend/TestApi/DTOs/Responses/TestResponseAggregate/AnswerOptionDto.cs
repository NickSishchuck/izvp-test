using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Responses.TestResponseAggregate
{

    /// <summary>
    /// DTO representing an answer option.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AnswerOptionDto
    {
        /// <summary>
        /// The unique identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Text content associated with this instance.
        /// </summary>
        public string Text { get; set; } = "";
    }
}
