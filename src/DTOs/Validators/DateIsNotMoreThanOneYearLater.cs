using System;
using BakuchiApi.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace BakuchiApi.Models.Dtos.Validators
{
    internal class DateIsNotMoreThanOneYearLater: ValidationAttribute
    {
        public string GetErrorMessage() 
            => $"Date must not be more than one year into the future";
        
        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            var date = (DateTime) value;
            var timeSpan = new TimeSpan(365, 0, 0, 0);
    
            if (date > DateTime.Now.Add(timeSpan))
            {
                return new ValidationResult(GetErrorMessage());
            }
            
            return ValidationResult.Success;
        }
    }
}