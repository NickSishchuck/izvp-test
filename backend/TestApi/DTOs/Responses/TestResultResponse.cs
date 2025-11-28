using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Responses
{
    /// <summary>
    /// Represents the calculated outcome of a completed test.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TestResultResponse
    {
        /// <summary>
        /// The number of questions answered correctly by the user.
        /// </summary>
        public int CorrectAnswers { get; set; }

        /// <summary>
        /// The total number of questions included in the test.
        /// </summary>
        public int TotalQuestions { get; set; }

        /// <summary>
        /// The score earned by the user based on correct answers.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The maximum possible score that can be achieved for the test.
        /// </summary>
        public int TotalScore { get; set; }
    }
}
