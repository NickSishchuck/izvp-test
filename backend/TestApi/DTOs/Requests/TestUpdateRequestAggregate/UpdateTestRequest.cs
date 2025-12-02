using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Requests.TestUpdateRequestAggregate
{
    [ExcludeFromCodeCoverage]
    public record UpdateTestRequest
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public List<UpdateQuestionDto> Questions { get; set; } = new();
    }
}
