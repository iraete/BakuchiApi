using FluentValidation;

namespace BakuchiApi.Controllers.Dtos.Validators
{
    public class CreatePoolDtoValidator : AbstractValidator<CreatePoolDto>
    {
        public CreatePoolDtoValidator()
        {
            RuleFor(p => p.EventId).NotNull();
            RuleFor(p => p.Alias)
                .NotNull()
                .MaximumLength(50);
            RuleFor(p => p.BetType).NotNull();
            RuleFor(p => p.Description).MaximumLength(200);
        }
    }

    public class UpdatePoolDtoValidator : AbstractValidator<UpdatePoolDto>
    {
        public UpdatePoolDtoValidator()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.Alias)
                .NotNull()
                .MaximumLength(50);
            RuleFor(p => p.Description).MaximumLength(200);
            RuleFor(p => p.BetType).NotNull();
        }
    }
}