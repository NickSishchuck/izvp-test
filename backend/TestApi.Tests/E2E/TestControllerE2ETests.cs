using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using TestApi.DTOs.Requests.TestSubmitRequestAggregate;
using TestApi.DTOs.Responses;
using TestApi.DTOs.Responses.TestResponseAggregate;

namespace TestApi.Tests.E2E
{
    public class TestControllerE2ETests
    {
        private readonly HttpClient _client;
        public TestControllerE2ETests()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTest_Returns_Ok_And_Data()
        {
            // act
            var response = await _client.GetAsync("/api/test");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var dto = await response.Content.ReadFromJsonAsync<TestResponse>();
            dto.Should().NotBeNull();
            dto.Title.Should().NotBeNullOrWhiteSpace();
            dto.Questions.Should().NotBeNull();
            dto.Questions.Should().NotBeEmpty();
        }

        [Fact]
        public async Task SubmitTest_Returns_Ok_With_Result()
        {
            // arrange
            var request = new TestSubmitRequest
            {
                UserName = Guid.NewGuid().ToString(),
                TestId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Title = "Тест з C# для початківців",
                Answers = new List<AnswerDto>
            {
                new()
                {
                    Id = 1,
                    SelectedOptionIds = new List<int> { 1 }
                },
                new()
                {
                    Id = 2,
                    SelectedOptionIds = new List<int> { 2, 3}
                },
                new()
                {
                    Id = 3,
                    SelectedOptionIds = new List<int>(),
                    TextAnswer = "Console.WriteLine"
                }
            }
            };

            // act
            var response = await _client.PostAsJsonAsync("/api/test/submit", request);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<TestResultResponse>();
            result.Should().NotBeNull();
            result!.TotalQuestions.Should().BeOfType(typeof(int));
            result.CorrectAnswers.Should().BeOfType(typeof(int));
            result.Score.Should().BeOfType(typeof(int));
            result.TotalScore.Should().BeOfType(typeof(int));
        }


        [Fact]
        public async Task SubmitTest_InvalidRequest_Returns_ValidationProblem()
        {
            // arrange
            var request = new TestSubmitRequest
            {
                UserName = "",
                TestId = Guid.Empty,
                Title = ""
            };

            // act
            var response = await _client.PostAsJsonAsync("/api/test/submit", request);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            problem.Should().NotBeNull();
            problem!.Errors.Should().ContainKey("Answers");
        }
    }

}

