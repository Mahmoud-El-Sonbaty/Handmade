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
        Task<ResultView<CRUDCategoriesDTO>> DeleteCategoryAsync(int id);
        Task<ResultView<List<GetAllCategoriesDTO>>> GetAllCategoriesAsync();
        Task<ResultView<GetOneCategoryDTO>> GetOneCategoryByIdAsync(int id);
        Task<ResultView<EntityPaginated<GetAllCategoriesDTO>>> GetPaginatedAsync(int pageNubmer, int pageSize);

    }
}
