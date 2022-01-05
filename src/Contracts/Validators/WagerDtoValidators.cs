using BakuchiApi.Contracts.Requests;
using FluentValidation;

namespace BakuchiApi.Contracts.Validators
{
    public class CreateWagerDtoValidator : AbstractValidator<CreateWagerDto>
    {
        public CreateWagerDtoValidator()
        {
            RuleFor(w => w.Name).NotEmpty();
            RuleFor(w => w.PoolId).NotNull();
            RuleFor(w => w.OutcomeId).NotNull();
            RuleFor(w => w.Amount).NotNull();
        }
    }

    public class UpdateWagerDtoValidator : AbstractValidator<UpdateWagerDto>
    {
        public UpdateWagerDtoValidator()
        {
            RuleFor(w => w.PoolId).NotNull();
            RuleFor(w => w.OutcomeId).NotNull();
            RuleFor(w => w.Amount).NotNull();
        }
    }
}