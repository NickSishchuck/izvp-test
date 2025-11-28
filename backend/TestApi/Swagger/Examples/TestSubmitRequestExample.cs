using Swashbuckle.AspNetCore.Filters;
using TestApi.DTOs.Requests.TestSubmitRequestAggregate;

namespace TestApi.Swagger.Examples
{
    public class TestSubmitRequestExample : IExamplesProvider<TestSubmitRequest>
    {
        public TestSubmitRequest GetExamples()
        {
            return new TestSubmitRequest
            {
                Title = "Тест з C# для початківців",
                UserName = "test_user",
                Answers = new List<AnswerDto>
                {
                    new AnswerDto
                    {
                        Id = 1,
                        SelectedOptionIds = new List<int> { 1 },
                        TextAnswer = null
                    },
                    new AnswerDto
                    {
                        Id = 2,
                        SelectedOptionIds = new List<int> { 1, 3 },
                        TextAnswer = null
                    },
                    new AnswerDto
                    {
                        Id = 3,
                        SelectedOptionIds = new List<int>(),
                        TextAnswer = "Console.WriteLine"
                    }
                }
            };
        }
    }
}
