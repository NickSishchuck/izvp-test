using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TestApi.DomainEntities;
using TestApi.DTOs.Responses;
using TestApi.Interfaces;
using TestApi.UseCases.Queries;

namespace TestApi.UseCases.Handlers
{
    public class GetTestQueryHandler(
        ILogger<GetTestQueryHandler> logger,
        IJsonSerializer jsonSerializer,
        IConfiguration configuration,
        IWebHostEnvironment env,
        IMapper mapper) : IRequestHandler<GetTestQuery, Result<TestResponseDto>>
    {
        public async Task<Result<TestResponseDto>> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Start handling GetTestQuery");

            // Read configuration for test file path from appsettings.json or appsettings.{Environment}.json
            var relativePath = configuration["TestPath"];
            logger.LogDebug("Config TestPath = {Path}", relativePath);

            // Combine with content root path to get full file path. Example: "C:\Projects\MyApp\Tests\test1.json"
            var fullPath = Path.Combine(env.ContentRootPath, relativePath ?? string.Empty);
            logger.LogDebug("Full test path = {FullPath}", fullPath);

            // Read JSON file content
            var json = await File.ReadAllTextAsync(fullPath, cancellationToken);
            logger.LogDebug("JSON: {Json}", json);

            // Deserialize JSON to domain entity
            var entity = jsonSerializer.FromJson<TestEntity>(json);
            logger.LogDebug("Deserialized title = {Title}, questions = {Count}", entity.Title, entity.Questions.Count);

            // Map domain entity to response DTO
            var dto = mapper.Map<TestResponseDto>(entity);
            logger.LogDebug("Mapped DTO questions = {Count}", dto.Questions.Count);

            logger.LogInformation("Finish handling GetTestQuery");
            return Result.Success(dto);
        }
    }
}
