using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class UserDisputes : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int OrderId { get; set; }
        //public Order Order { get; set; }

        [MaxLength(600)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Status { get; set; }
    }
}
