using System.Diagnostics.CodeAnalysis;

namespace TestApi.DomainEntities
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Represents a test entity that contains a title and a collection of questions.
    /// </summary>
    public class TestEntity
    {
        /// <summary>
        /// Title associated with the object.
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Collection of questions associated with this instance.
        /// </summary>
        public List<Question> Questions { get; set; } = new();
    }
}
