using CSharpFunctionalExtensions;
using MediatR;
using TestApi.DomainEntities;
using TestApi.Implementations.Repositories;
using TestApi.Interfaces;
using TestApi.UseCases.Commands;

namespace TestApi.UseCases.Handlers
{
    /// <summary>
    /// Handles the <see cref="UpdateTestCommand"/> to update the main test with new questions and options.
    /// </summary>
    public class UpdateTestCommandHandler(ITestRepository repository, ILogger<UpdateTestCommandHandler> logger) : IRequestHandler<UpdateTestCommand, Result<TestEntity>>
    {
        public async Task<Result<TestEntity>> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Start handling UpdateTestCommandHandler");

            var dto = request.Request;

            var test = new TestEntity()
            {
                Id = dto.Id,
                Title = dto.Title,
                Questions = dto.Questions
                .OrderBy(q => q.Id)
                .Select(q => new Question
                {
                    Id = q.Id,
                    Text = q.Text,
                    Type = (QuestionType)q.Type,
                    Score = q.Score,
                    CorrectTextAnswer = q.Text,
                    Options = q.Options
                    .OrderBy(q => q.Id)
                    .Select(o => new AnswerOption
                    {
                        Id = o.Id,
                        Text = o.Text,
                        IsCorrect = o.IsCorrect,
                    })
                    .ToList()
                })
                .ToList()
            };

            await repository.SaveMainTestAsync(test, CancellationToken.None);

            logger.LogInformation("UpdateTestCommandHandler handled successfully.");

            return Result.Success(test);
        }
    }
}
