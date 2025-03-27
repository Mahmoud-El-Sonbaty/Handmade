using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class UserCoupon : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int CouponId { get; set; }
        //public Coupon Coupon { get; set; }
    }
}
