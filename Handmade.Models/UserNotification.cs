using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class UserNotification : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        [MaxLength(256)]
        public string? Message { get; set; }
    }
}
