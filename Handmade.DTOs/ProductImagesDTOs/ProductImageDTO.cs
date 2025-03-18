using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.ProductImagesDTOs
{
   public class ProductImageDTO
    {
        
        public string ImageUrl { get; set; }

     
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public bool IsMain { get; set; } = false;

       
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
