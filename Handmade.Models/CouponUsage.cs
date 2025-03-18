using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class CouponUsage : BaseEntity<CouponUsage>
    {
        public int UsageId { get; set; }
        public int CouponId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public DateTime UsedOn { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
    }
}
