using Handmade.DTOs.CategoryDTOs;
using Handmade.DTOs.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<ResultView<CRUDCategoriesDTO>> CreateCategoryAsync(CRUDCategoriesDTO CategoryDTO);
        Task<ResultView<CRUDCategoriesDTO>> UpdateCategoryAsync(CRUDCategoriesDTO CategoryDTO);
        Task<ResultView<CRUDCategoriesDTO>> DeleteCategoryAsync(int id,string lang);
        Task<ResultView<List<GetAllCategoriesDTO>>> GetAllCategoriesAsync(string lang);
        Task<ResultView<GetOneCategoryDTO>> GetOneCategoryByIdAsync(int id , string lang,bool WithCild=false); 
        Task<ResultView<List<GetOneCategoryDTO>>> GetTheCategoriesByParentIdAsync(int id , string lang,bool WithCild=false);
        Task<ResultView<EntityPaginated<GetAllCategoriesDTO>>> GetPaginatedAsync(int pageNubmer, int pageSize ,string lang);

    }
}
