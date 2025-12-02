using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TestApi.DomainEntities;
using TestApi.DTOs.Requests.TestUpdateRequestAggregate;
using TestApi.DTOs.Responses.TestResponseAggregate;

namespace TestApi.Tests.E2E
{
    public class AdminControllerE2ETests
    {
        private readonly HttpClient _client;

        public AdminControllerE2ETests()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ChangeTest_Returns_Updated_Test()
        {
            // arrange
            var request = new UpdateTestRequest
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Title = "Тест з C# для початківців",
                Questions = new List<UpdateQuestionDto>
                {
                    new UpdateQuestionDto
                    {
                        Id = 1,
                        Text = "Який тип даних використовується для зберігання цілого числа в C#?",
                        Type = 0,
                        Score = 1,
                        Options = new()
                        {
                            new UpdateAnswerOptionDto { Id = 1, Text = "int",    IsCorrect = true  },
                            new UpdateAnswerOptionDto { Id = 2, Text = "string", IsCorrect = false },
                            new UpdateAnswerOptionDto { Id = 3, Text = "bool",   IsCorrect = false }
                        }
                    },
                    new UpdateQuestionDto
                    {
                        Id = 2,
                        Text = "Які з наведених типів є посилальними?",
                        Type = 1,
                        Score = 2,
                        Options = new()
                        {
                            new UpdateAnswerOptionDto { Id = 1, Text = "class",  IsCorrect = true  },
                            new UpdateAnswerOptionDto { Id = 2, Text = "struct", IsCorrect = false },
                            new UpdateAnswerOptionDto { Id = 3, Text = "string", IsCorrect = true  }
                        }
                    },
                    new UpdateQuestionDto
                    {
                        Id = 3,
                        Text = "Напишіть ключове слово для виведення тексту в консоль у C#.",
                        Type = 2,
                        Score = 2,
                        CorrectTextAnswer = "Console.WriteLine"
                    }
                }
            };

            // act
            var response = await _client.PutAsJsonAsync("/api/admin/change", request);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var dto = await response.Content.ReadFromJsonAsync<TestEntity>();
            dto.Should().NotBeNull();
            dto!.Id.Should().Be(request.Id);
        }
    }
}
