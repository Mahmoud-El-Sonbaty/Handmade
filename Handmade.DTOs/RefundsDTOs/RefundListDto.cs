using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.RefundsDTOs
{
    public class RefundListDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal RefundAmount { get; set; }
        public string RefundStatus { get; set; }
        public DateTime RefundDate { get; set; }
    }

}
