using System.ComponentModel.DataAnnotations;

namespace Handmade.DTOs.CategoryDTOs
{
    public class GetAllCategoriesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsParent { get; set; }
        public int ParentCategoryId { get; set; } 
        public int Level { get; set; }
        public string? CatogorylogoPath { get; set; }
        public List<GetAllCategoriesDTO>? Children { get; set; }


    }
}
