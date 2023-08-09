using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Validation
{
	public class CustomBirthDateValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? BirthDate = DateTime.Parse((string)value);
            if (BirthDate < DateTime.Now.AddYears(-21))
            {
                return ValidationResult.Success!;
            }
            return new ValidationResult("Age must not be less than 21 years");
        }
    }
}

