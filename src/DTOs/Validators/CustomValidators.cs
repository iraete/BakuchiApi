using System;
using FluentValidation;

namespace BakuchiApi.Controllers.Dtos.Validators
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, DateTime>
            DateIsNotMoreThanOneYearLater<T>
            (this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            var now = DateTime.Now;
            return ruleBuilder.Must(date => date < now.AddYears(1))
                .WithMessage("Date must be less than one year into the future");
        }
    }
}