using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.Validations
{
    public class ConfirmPasswordValidationAttribute(string passwordPropertyName) : ValidationAttribute
    {
        private readonly string _passwordPropertyName = passwordPropertyName;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var passwordProperty = validationContext.ObjectType.GetProperty(_passwordPropertyName);

            if (passwordProperty == null)
            {
                return new ValidationResult($"Unknown property: {_passwordPropertyName}");
            }

            var passwordValue = passwordProperty.GetValue(validationContext.ObjectInstance, null) as string;
            var confirmPasswordValue = value as string;

            if (passwordValue != confirmPasswordValue)
            {
                return new ValidationResult("Password and Confirm Password do not match.");
            }

            return ValidationResult.Success;
        }
    }
}
