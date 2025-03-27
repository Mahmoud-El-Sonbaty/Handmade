using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class Payment : BaseEntity<int>
    {
        public int OrderId { get; set; }
        //public Order? Order { get; set; }

        [Required(ErrorMessage = "Payment Date Is Required.")]
        public new DateTime CreatedAt { get; set; } // should this be new / override and make the base entity virtual / new propery name like PaymentDate
        public decimal Amount { get; set; }
        public int PaymentMethodId { get; set; }
        //public PaymentMethod? PaymentMethod { get; set; }
    }
}
