using System;
using BakuchiApi.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace BakuchiApi.Models.Dtos.Validators
{
    public class DateIsNotMoreThanOneYearLater: ValidationAttribute
    {
        public string GetNullMessage()
            => $"Date is null";

        public string GetErrorMessage() 
            => $"Date must not be more than one year into the future";
        
        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(GetNullMessage());
            }

            var date = (DateTime) value;
            
            if (date > DateTime.Now.AddYears(1))
            {
                return new ValidationResult(GetErrorMessage());
            }
            
            return ValidationResult.Success;
        }
    }
}