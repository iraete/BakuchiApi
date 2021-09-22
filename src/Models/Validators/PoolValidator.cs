using FluentValidation;

namespace BakuchiApi.Models.Validators
{
    public class PoolValidator : AbstractValidator<Pool>
    {
        public PoolValidator()
        {
            RuleFor(p => p.Alias).NotNull().MaximumLength(50);
            RuleFor(p => p.Description).MaximumLength(100);
            RuleFor(p => p.EventId).NotNull();
            RuleFor(p => p.BetType).NotNull();
        }
    }
}