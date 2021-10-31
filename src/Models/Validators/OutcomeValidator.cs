using FluentValidation;

namespace BakuchiApi.Models.Validators
{
    public class OutcomeValidator : AbstractValidator<Outcome>
    {
        public OutcomeValidator()
        {
            RuleFor(outcome => outcome.EventId).NotNull();
            RuleFor(outcome => outcome.Name).NotNull();
            RuleFor(outcome => outcome.Alias).NotNull();
        }
    }
}