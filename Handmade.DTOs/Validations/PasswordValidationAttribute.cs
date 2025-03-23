using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Handmade.DTOs.Validations
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        private static readonly int MinimumLength = 3;
        private static readonly bool RequireUppercase = false;
        private static readonly bool RequireLowercase = false;
        private static readonly bool RequireDigit = false;
        private static readonly bool RequireSpecialCharacter = false;
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }

            string password = value.ToString();

            // Check password requirements based on configuration
            var hasMinimumLength = password.Length >= MinimumLength;
            var hasUpperCase = !RequireUppercase || Regex.IsMatch(password, "[A-Z]");
            var hasLowerCase = !RequireLowercase || Regex.IsMatch(password, "[a-z]");
            var hasDigit = !RequireDigit || Regex.IsMatch(password, "[0-9]");
            var hasSpecialChar = !RequireSpecialCharacter || Regex.IsMatch(password, "[!@#$%^&*(),.?\":{}|<>]");

            return hasMinimumLength && hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The {name} must meet the following requirements: " +
                   $"Minimum length: {MinimumLength}, " +
                   $"Uppercase: {RequireUppercase}, " +
                   $"Lowercase: {RequireLowercase}, " +
                   $"Digit: {RequireDigit}, " +
                   $"Special character: {RequireSpecialCharacter}.";
        }
    }
}
