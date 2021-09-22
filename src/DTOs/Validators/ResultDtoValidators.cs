using FluentValidation;

namespace BakuchiApi.Controllers.Dtos.Validators
{
    public class CreateResultDtoValidator : AbstractValidator<CreateResultDto>
    {
        public CreateResultDtoValidator()
        {
            RuleFor(r => r.EventId).NotNull();
            RuleFor(r => r.OutcomeId).NotNull();
            RuleFor(r => r.Rank).NotNull();
        }
    }

    public class UpdateResultDtoValidator : AbstractValidator<UpdateResultDto>
    {
        public UpdateResultDtoValidator()
        {
            RuleFor(r => r.EventId).NotNull();
            RuleFor(r => r.OutcomeId).NotNull();
            RuleFor(r => r.Rank).NotNull();
        }
    }
}