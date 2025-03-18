using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class Order : BaseEntity<Order>
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } 
        public string PaymentMethod { get; set; } 
        public string ShippingAddress { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime? PaymentDate { get; set; }

        // العلاقات
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } // المنتجات في الطلب
        public virtual ICollection<Refund> Refunds { get; set; } // المرتجعات المرتبطة بالطلب
        public virtual ICollection<CouponUsage> CouponUsages { get; set; } // الكوبونات المستخدمة
    }

}
