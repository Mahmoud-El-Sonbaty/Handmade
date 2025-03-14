using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.CouponsDTOs
{
    public class CRUDCouponDTO
    {
        public string CouponCode { get; set; }
        public string DiscountType { get; set; } // "Percentage" or "FixedAmount"
        public decimal DiscountValue { get; set; }
        public decimal? MinOrderValue { get; set; } // الحد الأدنى لتطبيق الكوبون
        public decimal? MaxDiscount { get; set; } // أقصى قيمة للخصم****
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int? UsageLimit { get; set; } // عدد المرات التي يمكن استخدام الكوبون فيها****
        public int UsedCount { get; set; } // عدد مرات الاستخدام الحالية****
        public bool IsActive { get; set; } // حالة الكوبون (فعال أم لا)****
    }

}
