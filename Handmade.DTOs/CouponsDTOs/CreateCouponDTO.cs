using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.CouponsDTOs
{
    public class CreateCouponDTO
    {
        [Required]
        public string CouponCode { get; set; }

        [Required]
        [RegularExpression("Percentage|FixedAmount", ErrorMessage = "نوع الخصم يجب أن يكون 'Percentage' أو 'FixedAmount'.")]
        public string DiscountType { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "قيمة الخصم يجب أن تكون أكبر من 0.")]
        public decimal DiscountValue { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "يجب أن يكون الحد الأدنى للطلب قيمة صحيحة.")]
        public decimal? MinOrderValue { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "يجب أن يكون الحد الأقصى للخصم قيمة صحيحة.")]
        public decimal? MaxDiscount { get; set; }

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "يجب أن يكون الحد الأقصى لاستخدام الكوبون 1 أو أكثر.")]
        public int? UsageLimit { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
