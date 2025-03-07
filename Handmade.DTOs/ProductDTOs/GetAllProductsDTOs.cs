using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Handmade.DTOs.ProductDTOs
{
    public class GetAllProductsDTOs
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public decimal? Price { get; set; }
        public int StockQuantity { get; set; }
        public List<string> Images { get; set; }
    }
}
