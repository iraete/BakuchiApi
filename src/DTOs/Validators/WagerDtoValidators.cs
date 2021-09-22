using FluentValidation;

namespace BakuchiApi.Controllers.Dtos.Validators
{
    public class CreateWagerDtoValidator : AbstractValidator<CreateWagerDto>
    {
        public CreateWagerDtoValidator()
        {
            RuleFor(w => w.Name).NotNull();
            RuleFor(w => w.EventId).NotNull();
            RuleFor(w => w.PoolId).NotNull();
            RuleFor(w => w.OutcomeId).NotNull();
            RuleFor(w => w.Amount).NotNull();
        }
    }

    public class UpdateWagerDtoValidator : AbstractValidator<UpdateWagerDto>
    {
        public UpdateWagerDtoValidator()
        {
            RuleFor(w => w.Name).NotNull();
            RuleFor(w => w.EventId).NotNull();
            RuleFor(w => w.PoolId).NotNull();
            RuleFor(w => w.OutcomeId).NotNull();
            RuleFor(w => w.Amount).NotNull();
        }
    }
}