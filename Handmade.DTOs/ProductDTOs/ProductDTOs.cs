using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.ProductDTOs
{
     public class ProductDTOs
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public decimal? Price { get; set; }
        public int StockQuantity { get; set; }
        public List<string> Images { get; set; }
    }
}
