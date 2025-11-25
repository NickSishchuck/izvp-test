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
        /// </summary>
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        [Fact]
        public void Serialize_Test_ToJson_MatchExpected()
        {
            // arrange (setup testing data)
            var original = TestFactory.CreateUkrainianTest();

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
