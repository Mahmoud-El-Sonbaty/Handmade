using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models.ProductH
{
    public class ProductTag
    {
        public int TagID { get; set; }
        public string TagName { get; set; } = string.Empty;

        public List<ProductTagMappingH> ProductTagMappings { get; set; } = new List<ProductTagMappingH>();
    }

}
