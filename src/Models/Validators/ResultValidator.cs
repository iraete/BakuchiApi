using FluentValidation;

namespace BakuchiApi.Models.Validators
{
    public class ResultValidator : AbstractValidator<Result>
    {
        public ResultValidator()
        {
            RuleFor(result => result.EventId).NotNull();
            RuleFor(result => result.Alias).NotNull();
            RuleFor(result => result.Rank).NotNull();
        }
    }
}