using TestApi.DomainEntities;
using TestApi.DTOs.Requests.TestSubmitRequestAggregate;
using TestApi.DTOs.Responses;
using TestApi.Interfaces;

namespace TestApi.Implementations
{
    public class TestEvaluationService(ITestRepository testRepository) : ITestEvaluationService
    {
        public async Task<TestResultResponse> EvaluateAsync(
            TestSubmitRequest request,
            CancellationToken cancellationToken)
        {
            var test = await testRepository.LoadMainTest(cancellationToken);

            if (test is null || request.TestId != test.Id)
                throw new InvalidOperationException("Test not found");

            int correctCount = 0;
            int totalScore = 0;

            foreach (var question in test.Questions)
            {
                var userAnswer = request.Answers
                    .FirstOrDefault(a => a.Id == question.Id);

                if (userAnswer is null)
                    continue;

                bool isCorrect = question.Type switch
                {
                    QuestionType.SingleChoice => CheckSingleChoice(question, userAnswer),
                    QuestionType.MultipleChoice => CheckMultipleChoice(question, userAnswer),
                    QuestionType.Text => CheckText(question, userAnswer),
                    _ => false
                };

                if (isCorrect)
                {
                    correctCount++;
                    totalScore += question.Score;
                }
            }

            return new TestResultResponse
            {
                CorrectAnswers = correctCount,
                TotalQuestions = test.Questions.Count,
                Score = totalScore,
                TotalScore = test.Questions.Sum(a => a.Score)
            };
        }

        private static bool CheckSingleChoice(Question question, AnswerDto answer)
        {
            var correctOptionId = question.Options.First(o => o.IsCorrect).Id;
            var selected = answer.SelectedOptionIds ?? new List<int>();
            return selected.Count == 1 && selected[0] == correctOptionId;
        }

        private static bool CheckMultipleChoice(Question question, AnswerDto answer)
        {
            var correctIds = question.Options
                .Where(o => o.IsCorrect)
                .Select(o => o.Id)
                .OrderBy(x => x)
                .ToList();

            var selected = (answer.SelectedOptionIds ?? new List<int>())
                .OrderBy(x => x)
                .ToList();

            return correctIds.SequenceEqual(selected);
        }

        private static bool CheckText(Question question, AnswerDto answer)
        {
            if (string.IsNullOrWhiteSpace(question.CorrectTextAnswer))
                return false;

            var expected = question.CorrectTextAnswer.Trim();
            var actual = (answer.TextAnswer ?? string.Empty).Trim();

            return string.Equals(expected, actual, StringComparison.OrdinalIgnoreCase);
        }
    }
}
