using BakuchiApi.Contracts.Requests;
using FluentValidation;

namespace BakuchiApi.Contracts.Validators
{
    public class CreateOutcomeDtoValidator : AbstractValidator<CreateOutcomeDto>
    {
        public CreateOutcomeDtoValidator()
        {
            RuleFor(o => o.EventId).NotNull();
            RuleFor(o => o.Alias)
                .NotNull()
                .MaximumLength(50);
            RuleFor(o => o.Name).NotNull();
        }
    }

    public class UpdateOutcomeDtoValidator : AbstractValidator<UpdateOutcomeDto>
    {
        public UpdateOutcomeDtoValidator()
        {
            RuleFor(o => o.EventId).NotNull();
            RuleFor(o => o.Alias)
                .NotNull()
                .MaximumLength(50);
            RuleFor(o => o.Name).NotNull();
        }
    }
}