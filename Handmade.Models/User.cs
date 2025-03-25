using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class User : IdentityUser<int>
    {
        [MaxLength(20)]
        [Required(ErrorMessage = "First Name is required.")]
        public required string FirstName { get; set; }

        [MaxLength(20)]
        public string? LastName { get; set; }

        [MaxLength(800)]
        public string? Bio { get; set; }

        [MaxLength(1000)]
        public string? ImageUrl { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}
