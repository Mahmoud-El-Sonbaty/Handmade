using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Handmade.DTOs.CategoryDTOs
{
    public class CRUDCategoriesDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Of Catogory is required.")]
        [StringLength(50, MinimumLength = 4)]
        public string ArName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string EnName { get; set; }
        public bool IsParent { get; set; } = false;
        public int ParentCategoryId { get; set; }
        public int Level { get; set; }
        [StringLength(100, MinimumLength = 10)]

        public IFormFile? ICatogorylogoData { get; set; }

        public string? CatogorylogoPath { get; set; }
    }
}
