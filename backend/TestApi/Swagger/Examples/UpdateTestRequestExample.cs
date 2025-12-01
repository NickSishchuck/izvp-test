using Swashbuckle.AspNetCore.Filters;
using TestApi.DomainEntities;

namespace TestApi.Swagger.Examples
{
    public class UpdateTestRequestExample : IExamplesProvider<TestEntity>
    {
        public TestEntity GetExamples()
        {
            return new TestEntity
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Title = "Тест з C# для початківців",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = 1,
                        Text = "Який тип даних використовується для зберігання цілого числа в C#?",
                        Type = QuestionType.SingleChoice,
                        Score = 1,
                        Options = new()
                        {
                            new AnswerOption { Id = 1, Text = "int",    IsCorrect = true  },
                            new AnswerOption { Id = 2, Text = "string", IsCorrect = false },
                            new AnswerOption { Id = 3, Text = "bool",   IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        Id = 2,
                        Text = "Які з наведених типів є посилальними?",
                        Type = QuestionType.MultipleChoice,
                        Score = 2,
                        Options = new()
                        {
                            new AnswerOption { Id = 1, Text = "class",  IsCorrect = true  },
                            new AnswerOption { Id = 2, Text = "struct", IsCorrect = false },
                            new AnswerOption { Id = 3, Text = "string", IsCorrect = true  }
                        }
                    },
                    new Question
                    {
                        Id = 3,
                        Text = "Напишіть ключове слово для виведення тексту в консоль у C#.",
                        Type = QuestionType.Text,
                        Score = 2,
                        CorrectTextAnswer = "Console.WriteLine"
                    }
                }
            };
        }
    }
}
