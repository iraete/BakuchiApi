using BakuchiApi.Contracts.Requests;
using FluentValidation;

namespace BakuchiApi.Contracts.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(u => u.Id).NotNull();
            RuleFor(u => u.Name).NotNull();
        }
    }

    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(u => u.Id).NotNull();
            RuleFor(u => u.Name).NotNull();
            RuleFor(u => u.Balance).NotNull();
            RuleFor(u => u.LastRewardTime).NotNull();
        }
    }
    
    public class UpdateUserInfoDtoValidator : AbstractValidator<UpdateUserInfoDto>
    {
        public UpdateUserInfoDtoValidator()
        {
            RuleFor(u => u.Id).NotNull();
            RuleFor(u => u.Name).NotNull();
        }
    }
}