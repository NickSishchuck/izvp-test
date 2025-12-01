using FluentValidation;
using TestApi.DTOs.Requests.TestSubmitRequestAggregate;

namespace TestApi.Validators
{
    /// <summary>
    /// Provides validation rules for a <see cref="TestSubmitRequest"/>.
    /// </summary>
    public class TestSubmitRequestValidator : AbstractValidator<TestSubmitRequest>
    {
        public TestSubmitRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required")
                .MaximumLength(100);

            RuleFor(x => x.TestId)
                .NotNull()
                .WithMessage("Test id required");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Test name is required")
                .MaximumLength(200);

            RuleFor(x => x.Answers)
                .NotNull()
                .WithMessage("Answers are required")
                .NotEmpty()
                .WithMessage("At least one answer");

            RuleForEach(x => x.Answers)
                .SetValidator(new AnswerDtoValidator());
        }
    }
}
