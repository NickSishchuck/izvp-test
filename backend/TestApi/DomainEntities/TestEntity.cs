using System.Diagnostics.CodeAnalysis;

namespace TestApi.DomainEntities
{
    /// <summary>
    /// Represents a test entity that contains a title and a collection of questions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TestEntity
    {
        /// <summary>
        /// Unique id for test
        /// </summary>
        public Guid Id { get; set; }

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
