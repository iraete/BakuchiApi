using BakuchiApi.Contracts.Requests;
using FluentValidation;

namespace BakuchiApi.Contracts.Validators
{
    public class CreateResultDtoValidator : AbstractValidator<CreateResultDto>
    {
        public CreateResultDtoValidator()
        {
            RuleFor(r => r.EventId).NotNull();
            RuleFor(r => r.Alias).NotNull();
            RuleFor(r => r.Rank).NotNull();
        }
    }

    public class UpdateResultDtoValidator : AbstractValidator<UpdateResultDto>
    {
        public UpdateResultDtoValidator()
        {
            RuleFor(r => r.EventId).NotNull();
            RuleFor(r => r.Alias).NotNull();
            RuleFor(r => r.Rank).NotNull();
        }
    }
}