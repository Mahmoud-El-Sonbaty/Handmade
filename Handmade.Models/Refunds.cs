using Handmade.Models.ProductH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class Refund : BaseEntity<Refund>
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int? ProductId { get; set; } 
        public decimal RefundAmount { get; set; }
        public string RefundStatus { get; set; }
        public string RefundReason { get; set; }

        public DateTime RefundDate { get; set; }
        public int? ApprovedBy { get; set; } 
        public string PaymentMethod { get; set; }

        //public virtual Order Order { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
