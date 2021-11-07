using FluentValidation;

namespace BakuchiApi.Models.Validators
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(e => e.Name).NotNull();
            RuleFor(e => e.Alias)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(e => e.Description).MaximumLength(200);
            RuleFor(e => e.Start).NotEmpty();
            RuleFor(e => e.End).NotEmpty();
        }
    }
}