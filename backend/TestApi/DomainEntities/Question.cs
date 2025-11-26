using System.Diagnostics.CodeAnalysis;

namespace TestApi.DomainEntities
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Represents a question entity within the application domain.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Text content.
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// Type of the question.
        /// </summary>
        public QuestionType Type { get; set; }

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
        public List<AnswerOption> Options { get; set; } = new();
    }
}
