using System;
using System.Collections.Generic;
using System.Text;
using TestApi.DomainEntities;

namespace TestApi.Tests
{
    /// <summary>
    /// Provides factory methods for creating <see cref="TestEntity"/> for use in testing or demonstration scenarios.
    /// </summary>
    internal static class TestFactory
    {
        /// <summary>
        /// Creates a sample test entity containing beginner-level C# questions in Ukrainian.
        /// </summary>
        /// <returns>A <see cref="TestEntity"/> instance pre-populated with Ukrainian-language questions and answer options
        /// suitable for C# beginners.</returns>
        public static TestEntity CreateUkrainianTest()
        {
            return new TestEntity
            {
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
