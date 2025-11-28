using AutoMapper;
using TestApi.DomainEntities;
using TestApi.DTOs.Responses.TestResponseAggregate;

namespace TestApi.Mappers
{
    /// <summary>
    /// AutoMapper profile for mapping Test-related entities and DTOs.
    /// </summary>
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            // TestEntity -> TestResponseDto
            CreateMap<TestEntity, TestResponse>();

            // Question -> QuestionDto
            CreateMap<Question, QuestionDto>()
                // enum -> string
                .ForMember(d => d.Type,
                    opt => opt.MapFrom(s => s.Type.ToString()));

            // AnswerOption -> AnswerOptionDto
            CreateMap<AnswerOption, AnswerOptionDto>();
        }
    }
}
