using System.Text.Json;
using System.Text.Json.Serialization;
using TestApi.DomainEntities;
using FluentAssertions; // For fluent assertions in tests (human readable assertions)

namespace TestApi.Tests
{
    /// <summary>
    /// Contains unit tests that verify the correctness of JSON serialization and question structure for test entities.
    /// </summary>
    public class SerializationTests
    {
        /// <summary>
        /// Provides default options for JSON serialization, including enum conversion to strings and no indentation.
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = false,
            Converters = { new JsonStringEnumConverter() }
        };

        [Fact]
        public void Serialize_Test_ToJson_MatchExpected()
        {
            // arrange (setup testing data)
            var original = TestFactory.CreateUkrainianTest();

            var expectedJson =
                "{\"Title\":\"Тест з C# для початківців\",\"Questions\":["
              + "{\"Id\":1,"
              + "\"Text\":\"Який тип даних використовується для зберігання цілого числа в C#?\","
              + "\"Type\":\"SingleChoice\","
              + "\"CorrectTextAnswer\":null,"
              + "\"Options\":["
                  + "{\"Id\":1,\"Text\":\"int\",\"IsCorrect\":true},"
                  + "{\"Id\":2,\"Text\":\"string\",\"IsCorrect\":false},"
                  + "{\"Id\":3,\"Text\":\"bool\",\"IsCorrect\":false}"
              + "]},"
              + "{\"Id\":2,"
              + "\"Text\":\"Які з наведених типів є посилальними?\","
              + "\"Type\":\"MultipleChoice\","
              + "\"CorrectTextAnswer\":null,"
              + "\"Options\":["
                  + "{\"Id\":1,\"Text\":\"class\",\"IsCorrect\":true},"
                  + "{\"Id\":2,\"Text\":\"struct\",\"IsCorrect\":false},"
                  + "{\"Id\":3,\"Text\":\"string\",\"IsCorrect\":true}"
              + "]},"
              + "{\"Id\":3,"
              + "\"Text\":\"Напишіть ключове слово для виведення тексту в консоль у C#.\","
              + "\"Type\":\"Text\","
              + "\"CorrectTextAnswer\":\"Console.WriteLine\","
              + "\"Options\":[]}"
              + "]}";

            // act (perform the action to be tested)
            var json = JsonSerializer.Serialize(original, _options);
            var deserialized = JsonSerializer.Deserialize<TestEntity>(json, _options);

            // assert (verify the result)
            deserialized.Should().NotBeNull();
            deserialized.Should().BeEquivalentTo(original);
        }

        [Fact]
        public void Questions_Should_Have_Correct_AnswerStructure()
        {
            // arrange (setup testing data)
            var test = TestFactory.CreateUkrainianTest();

            // act & assert (perform the action to be tested and verify the result)
            var single = test.Questions.Single(q => q.Id == 1);
            single.Type.Should().Be(QuestionType.SingleChoice);
            single.Options.Count(o => o.IsCorrect).Should().Be(1);

            var multiple = test.Questions.Single(q => q.Id == 2);
            multiple.Type.Should().Be(QuestionType.MultipleChoice);
            multiple.Options.Count(o => o.IsCorrect).Should().Be(2);

            var text = test.Questions.Single(q => q.Id == 3);
            text.Type.Should().Be(QuestionType.Text);
            text.CorrectTextAnswer.Should().Be("Console.WriteLine");
        }
    }
}
