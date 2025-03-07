using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models.ProductH
{
   public class ProductTagMappingH
    {
        public int ProductID { get; set; } 
        public Product Product { get; set; }

        public int TagID { get; set; }
        public ProductTag ProductTag { get; set; }
    }
}
