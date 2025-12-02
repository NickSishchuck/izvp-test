using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TestApi.DomainEntities;
using TestApi.DTOs.Responses.TestResponseAggregate;
using TestApi.Interfaces;
using TestApi.UseCases.Queries;

namespace TestApi.UseCases.Handlers
{
    /// <summary>
    /// Handles the <see cref="GetTestQuery"/> to retrieve the main test with all its questions.
    /// </summary>
    public class GetTestQueryHandler(
        ILogger<GetTestQueryHandler> logger,
        ITestRepository repository,
        IMapper mapper) : IRequestHandler<GetTestQuery, Result<TestResponse>>
    {
        public async Task<Result<TestResponse>> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Start handling GetTestQuery");

            var entity = await repository.LoadMainTest(cancellationToken);

            // Map domain entity to response DTO
            var dto = mapper.Map<TestResponse>(entity);
            logger.LogDebug("Mapped DTO questions = {Count}", dto.Questions.Count);

            logger.LogInformation("Finish handling GetTestQuery");
            return Result.Success(dto);
        }
    }
}
