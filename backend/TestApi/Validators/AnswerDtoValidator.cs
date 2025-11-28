using FluentValidation;
using TestApi.DTOs.Requests.TestSubmitRequestAggregate;

namespace TestApi.Validators
{
    /// <summary>
    /// Provides validation rules for a single answer within a <see cref="TestSubmitRequest"/>.
    /// </summary>
    public class AnswerDtoValidator : AbstractValidator<AnswerDto>
    {
        public AnswerDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(-1)
                .WithMessage("Id must be 0 or greater");

            RuleFor(x => x)
                .Must(a =>
                    (a.SelectedOptionIds != null && a.SelectedOptionIds.Any())
                    || !string.IsNullOrWhiteSpace(a.TextAnswer))
                .WithMessage("Answer is required");

        }
    }
}