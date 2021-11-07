using BakuchiApi.Contracts.Requests;
using FluentValidation;

namespace BakuchiApi.Contracts.Validators
{
    public class CreateEventDtoValidator : AbstractValidator<CreateEventDto>
    {
        public CreateEventDtoValidator()
        {
            RuleFor(e => e.Name).NotNull();
            RuleFor(e => e.UserName).NotNull();
            RuleFor(e => e.Alias).NotNull().MaximumLength(50);
            RuleFor(e => e.Description).MaximumLength(200);
            RuleFor(e => e.Start)
                .NotNull()
                .DateIsNotMoreThanOneYearLater();
            RuleFor(e => e.End)
                .NotNull()
                .DateIsNotMoreThanOneYearLater();
        }
    }

    public class UpdateEventDtoValidator : AbstractValidator<UpdateEventDto>
    {
        public UpdateEventDtoValidator()
        {
            RuleFor(e => e.Id).NotNull();
            RuleFor(e => e.Description).MaximumLength(200);
            RuleFor(e => e.Start)
                .NotNull()
                .DateIsNotMoreThanOneYearLater();
            RuleFor(e => e.End)
                .NotNull()
                .DateIsNotMoreThanOneYearLater();
        }
    }
}