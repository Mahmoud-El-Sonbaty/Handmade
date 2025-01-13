using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.CategoryDTOs
{
    public class GetOneCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsParent { get; set; }
        public int ParentCategoryId { get; set; }
        public int Level { get; set; }
        public string? CatogorylogoPath { get; set; }

    }
}
