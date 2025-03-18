using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.RefundsDTOs
{
    public class RefundCreateDto
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
    }

}
