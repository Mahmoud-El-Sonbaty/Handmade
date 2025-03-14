using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class Coupon : BaseEntity<Refund>
    {
        public string CouponCode { get; set; }
        public string DiscountType { get; set; } // "Percentage" or "FixedAmount"
        public decimal DiscountValue { get; set; }
        public decimal? MinOrderValue { get; set; } // الحد الأدنى لتطبيق الكوبون
        public decimal? MaxDiscount { get; set; } // أقصى قيمة للخصم
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int? UsageLimit { get; set; } // عدد المرات التي يمكن استخدام الكوبون فيها
        public int UsedCount { get; set; } = 0; // عدد مرات الاستخدام الحالية
        public bool IsActive { get; set; } = true;

        // العلاقة مع عمليات الاستخدام
        public virtual ICollection<CouponUsage> CouponUsages { get; set; }
    }

}
