using FluentAssertions;
using TestApi.DTOs.Requests.TestSubmitRequestAggregate;
using TestApi.Validators;

namespace TestApi.Tests
{
    public class TestSubmitRequestValidatorTests
    {
        private readonly TestSubmitRequestValidator _validator = new();

        [Fact]
        public async Task Valid_Request_Should_Pass_Validation()
        {
            // Arrange
            var request = new TestSubmitRequest
            {
                UserName = "test_user",
                Title = "Тест з C# для початківців",
                Answers = new List<AnswerDto>
                {
                    new AnswerDto { Id = 1, SelectedOptionIds = new List<int> { 1 } },
                    new AnswerDto { Id = 2, SelectedOptionIds = new List<int> { 1, 3 } },
                    new AnswerDto { Id = 3, TextAnswer = "Console.WriteLine" }
                }
            };

            // Act
            var result = await _validator.ValidateAsync(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Empty_Username_Should_Fail_Validation()
        {
            var request = new TestSubmitRequest
            {
                UserName = "",
                Title = "Тест з C# для початківців",
                Answers = new List<AnswerDto>
                {
                    new AnswerDto { Id = 1, SelectedOptionIds = new List<int> { 1 } }
                }
            };

            var result = await _validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "UserName");
        }

        [Fact]
        public async Task Missing_Answers_Should_Fail_Validation()
        {
            var request = new TestSubmitRequest
            {
                UserName = "test_user",
                Title = "Тест з C# для початківців",
                Answers = new List<AnswerDto>()
            };

            var result = await _validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Answers");
        }

        [Fact]
        public async Task Answer_Without_Options_And_Text_Should_Fail_Validation()
        {
            var request = new TestSubmitRequest
            {
                UserName = "test_user",
                Title = "Тест з C# для початківців",
                Answers = new List<AnswerDto>
                {
                    new AnswerDto { Id = 1 }
                }
            };

            var result = await _validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName.StartsWith("Answers[0]"));
        }
    }
}
