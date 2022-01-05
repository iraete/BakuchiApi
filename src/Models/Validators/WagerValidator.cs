using FluentValidation;

namespace BakuchiApi.Models.Validators
{
    public class WagerValidator : AbstractValidator<Wager>
    {
        public WagerValidator()
        {
            RuleFor(wager => wager.UserId).NotEmpty();
            RuleFor(wager => wager.PoolId).NotEmpty();
            RuleFor(wager => wager.OutcomeId).NotEmpty();
            RuleFor(wager => wager.BetType).NotEmpty();
        }
    }
}