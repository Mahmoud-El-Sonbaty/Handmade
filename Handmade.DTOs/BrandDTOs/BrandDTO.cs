using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.BrandDTOs
{
    public class BrandDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BrandDTO()
        {
            
        }
        public BrandDTO(CreateBrandDTO createBrandDTO)
        {
            Name = createBrandDTO.Name;
        }
    }
}
