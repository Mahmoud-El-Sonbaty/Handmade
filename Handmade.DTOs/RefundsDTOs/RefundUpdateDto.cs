using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.RefundsDTOs
{

    public class RefundUpdateDto
    {
        public int Id { get; set; } // معرف الاسترداد لتحديد السجل عند التحديث
        public string RefundStatus { get; set; }
        public string RefundReason { get; set; }
        public decimal RefundAmount { get; set; }
        public int? ApprovedBy { get; set; }
    }

}
