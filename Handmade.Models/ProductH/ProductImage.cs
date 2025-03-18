using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models.ProductH
{
   public class ProductImage : BaseEntity<ProductImage>
    {
         

        [Required(ErrorMessage = "Image URL is required.")]
        [StringLength(255, ErrorMessage = "URL cannot exceed 255 characters.")]
        public string ImageUrl { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; } 

        [Display(Name = "Is Main Image")]
        public bool IsMain { get; set; } = false;  

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
