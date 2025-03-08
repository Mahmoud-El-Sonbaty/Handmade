using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models.ProductH
{
   public class Product : BaseEntity<Product>

    {

     public string Name { get; set; } = string.Empty;  
     public string? Description { get; set; } 

    [Column(TypeName = "money")]
    [Range(0, double.MaxValue, ErrorMessage = "Product price must be a positive value.")]
    public decimal? Price { get; set; }
    public int StockQuantity { get; set; }
    //public ICollection<ProductCategoryB> ProductCategories { get; set; }
    public ICollection<ProductImage> Images { get; set; }
        //public ICollection<OrderItemB> OrderItems { get; set; }
        //public ICollection<ProductSpecificationsB> Specifications { get; set; }
        //public ICollection<ProductTranslationB> Translations { get; set; }
     public List<ProductTagMapping> ProductTagMappings { get; set; } = new List<ProductTagMapping>();







    }

}

