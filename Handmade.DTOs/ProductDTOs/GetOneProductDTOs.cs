using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.ProductDTOs
{
   public  class GetOneProductDTOs
    {
        public int Id { get; set; }

        public string? Name { get; set; } 
        public string? Description { get; set; }

        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public List<string> Images { get; set; }
    }
}
