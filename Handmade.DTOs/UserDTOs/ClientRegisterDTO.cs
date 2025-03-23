using Handmade.DTOs.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.UserDTOs
{
    public class ClientRegisterDTO
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [PasswordValidation]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [ConfirmPasswordValidation(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
