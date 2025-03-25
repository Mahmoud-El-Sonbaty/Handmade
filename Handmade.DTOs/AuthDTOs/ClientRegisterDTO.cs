using Handmade.DTOs.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.AuthDTOs
{
    public class ClientRegisterDTO
    {
        [MaxLength(20)]
        [Required(ErrorMessage = "First Name is required.")]
        public required string FirstName { get; set; }

        [MaxLength(20)]
        //[Required(ErrorMessage = "Last Name is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [PasswordValidation]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [ConfirmPasswordValidation(nameof(Password))]
        public required string ConfirmPassword { get; set; }
    }
}
