using System.Diagnostics.CodeAnalysis;
using TestApi.DTOs.Entities;

namespace TestApi.DTOs.Responses
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// DTO representing a test response.
    /// </summary>
    public class TestResponseDto
    {
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
