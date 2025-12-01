using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Requests.TestUpdateRequestAggregate
{
    [ExcludeFromCodeCoverage]
    public class UpdateTestRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public List<UpdateQuestionDto> Questions { get; set; }
    }
}
