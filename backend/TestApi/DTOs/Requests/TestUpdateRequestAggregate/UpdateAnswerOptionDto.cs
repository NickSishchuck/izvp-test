using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Requests.TestUpdateRequestAggregate
{
    [ExcludeFromCodeCoverage]
    public class UpdateAnswerOptionDto
    {
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Text content associated with this instance.
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// Value indicating whether the answer is correct.
        /// </summary>
        public bool IsCorrect { get; set; }
    }
}