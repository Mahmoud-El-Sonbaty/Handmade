using Handmade.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.ProductTagsDTOs
{
   public class CRUDProductTagDTOs
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        public List<CRUDProductDTOs> Products { get; set; } = new List<CRUDProductDTOs>();
    }
}
