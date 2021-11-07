using FluentValidation;

namespace BakuchiApi.Models.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id).GreaterThan(0);
            RuleFor(user => user.Name).NotNull();
        }
    }
}