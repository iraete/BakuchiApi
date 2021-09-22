using FluentValidation;

namespace BakuchiApi.Controllers.Dtos.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(u => u.Name).NotNull();
        }
    }

    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(u => u.Id).NotNull();
            RuleFor(u => u.Name).NotNull();
        }
    }
}