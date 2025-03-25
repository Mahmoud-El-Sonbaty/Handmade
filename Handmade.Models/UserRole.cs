using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public IdentityRole<int>? Role { get; set; }
        public User? User { get; set; }
    }
}
