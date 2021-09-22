using FluentValidation;

namespace BakuchiApi.Models.Validators
{
    public class WagerValidator : AbstractValidator<Wager>
    {
        public WagerValidator()
        {
            RuleFor(wager => wager.UserId).NotNull();
            RuleFor(wager => wager.PoolId).NotNull();
            RuleFor(wager => wager.Amount).NotNull();
            RuleFor(wager => wager.OutcomeId).NotNull();
            RuleFor(wager => wager.BetType).NotNull();            
            RuleFor(wager => wager.EventId).NotNull();            
        }
    }
}