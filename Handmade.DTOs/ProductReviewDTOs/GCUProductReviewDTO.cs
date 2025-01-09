using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.ProductReviewDTOs
{
    public class GCUProductReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
