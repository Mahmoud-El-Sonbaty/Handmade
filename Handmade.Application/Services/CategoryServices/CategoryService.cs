using AutoMapper;
using Handmade.Application.Contracts;
using Handmade.DTOs.CategoryDTOs;
using Handmade.DTOs.ProductReviewDTOs;
using Handmade.DTOs.SharedDTOs;
using Handmade.Models;

namespace Handmade.Application.Services.CategoryServices
{
    public class CategoryService(ICatogoryRepository catogoryRepository, IMapper mapper) : ICategoryService
    {
        
        // CRUD Operation

        public async Task<ResultView<CRUDCategoriesDTO>> CreateCategoryAsync(CRUDCategoriesDTO CategoryDTO)
        {

            try
            {
                var allCategories = await catogoryRepository.GetAllAsync();

                if (CategoryDTO == null)
                {
                    return new ResultView<CRUDCategoriesDTO> { IsSuccess = false, Msg = "Wrong Data" };

                }
                else if (allCategories.Any(Cat => Cat.Id == CategoryDTO.Id))
                {

                    return new ResultView<CRUDCategoriesDTO> { IsSuccess = false, Msg = "This Category Is Already Exist." };



                }
                else if (allCategories.Any(Cat => CategoryDTO.EnName.StartsWith(Cat.EnName, StringComparison.OrdinalIgnoreCase)
                                              ||  CategoryDTO.ArName.StartsWith(Cat.ArName, StringComparison.OrdinalIgnoreCase)))
                {
                    return new ResultView<CRUDCategoriesDTO> { IsSuccess = false, Msg = "This Category's Name Is Already Exist." };

                }
                else
                {
                    Category category = mapper.Map<Category>(CategoryDTO);
                    Category createdCategory = await catogoryRepository.CreateAsync(category);
                    await catogoryRepository.SaveChangesAsync();
                    CRUDCategoriesDTO result = mapper.Map<CRUDCategoriesDTO>(createdCategory);
                    return new ResultView<CRUDCategoriesDTO> { Data = result, IsSuccess = true ,Msg="Category Created Successfuly"};
                }


            }
            catch (Exception ex)
            {

                return new ResultView<CRUDCategoriesDTO> { Data=null, IsSuccess = false, Msg = ex.Message };
            }
        }

        public async Task<ResultView<CRUDCategoriesDTO>> UpdateCategoryAsync(CRUDCategoriesDTO CategoryDTO)
        {
            try
            {
                var allCategories = await catogoryRepository.GetAllAsync();

                if (CategoryDTO == null || CategoryDTO.Id <= 0)
                {
                    return new ResultView<CRUDCategoriesDTO> { IsSuccess = false, Msg = "Wrong Data" };

                }
                else if (!allCategories.Any(Cat => Cat.Id == CategoryDTO.Id))
                {

                    return new ResultView<CRUDCategoriesDTO> { IsSuccess = false, Msg = "This Category Is Not Exist." };



                }
                else
                {
                    Category category = mapper.Map<Category>(CategoryDTO);
                    Category createdCategory = await catogoryRepository.UpdateAsync(category);
                    await catogoryRepository.SaveChangesAsync();
                    CRUDCategoriesDTO result = mapper.Map<CRUDCategoriesDTO>(createdCategory);
                    return new ResultView<CRUDCategoriesDTO> { Data = result, IsSuccess = true, Msg = "Category Updated Successfuly" };
                }


            }
            catch (Exception ex)
            {

                return new ResultView<CRUDCategoriesDTO> { Data = null, IsSuccess = false, Msg = ex.Message };

            }
        }
        public  async Task<ResultView<CRUDCategoriesDTO>> DeleteCategoryAsync(int id)
        {
            var allCategories = await catogoryRepository.GetAllAsync();

            try
            {
                if (id <= 0)
                {
                    return new ResultView<CRUDCategoriesDTO> { IsSuccess = false, Msg = "Wrong Id" };
                }
                else if (!allCategories.Any(cat => cat.Id == id))
                {
                    return new ResultView<CRUDCategoriesDTO> { IsSuccess = false, Msg = "This Category Not Found!." };

                }
                else {
                    var cat=   allCategories.FirstOrDefault(Id => Id.Id == id);
                    await catogoryRepository.DeleteAsync(cat);
                    await catogoryRepository.SaveChangesAsync();
                    CRUDCategoriesDTO result = mapper.Map<CRUDCategoriesDTO>(cat);
                    return new ResultView<CRUDCategoriesDTO> { Data = result, IsSuccess = true ,Msg=$"Category with Name {result.EnName} is Deleted Succssfully" };

                }
            }
            catch (Exception ex) { 
            
                return new ResultView<CRUDCategoriesDTO> { Msg = ex.Message };
            }
        }


        // Get All & Get one 
        public async Task<ResultView<List<GetAllCategoriesDTO>>> GetAllCategoriesAsync()
        {
            try
            {
                List<Category> categories = [.. (await catogoryRepository.GetAllAsync()).Where(a => a.IsDeleted != true)];
                if( categories==null)
                {
                    return new ResultView<List<GetAllCategoriesDTO>> {  IsSuccess = false ,Msg="Non Categories Found!" };

                }
                List<GetAllCategoriesDTO> result = mapper.Map<List<GetAllCategoriesDTO>>(categories);
                return new ResultView<List<GetAllCategoriesDTO>> { Data = result, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResultView<List<GetAllCategoriesDTO>> { IsSuccess = false, Msg = ex.Message };
            }
        }

        public async Task<ResultView<GetOneCategoryDTO>> GetOneCategoryByIdAsync(int id)
        {
            
            try
            {
                Category categories = (await catogoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == id && !c.IsDeleted);
                if (categories is not null)
                {
                    GetOneCategoryDTO result = mapper.Map<GetOneCategoryDTO>(categories);
                    return new ResultView<GetOneCategoryDTO> { Data = result, IsSuccess = true ,Msg=$"Get {result.Name} Category Succssfully" };
                    
                }
                else
                {
                 return new ResultView<GetOneCategoryDTO> { IsSuccess = false, Msg = "Category Not Found!" };

                }
            }
            catch (Exception ex)
            {
                return new ResultView<GetOneCategoryDTO> { IsSuccess = false, Msg = $"Error Happen While find Category " + ex.Message };
            }
        }

        public async Task<ResultView<EntityPaginated<GetAllCategoriesDTO>>> GetPaginatedAsync(int pageNubmer, int pageSize)
        {
            ResultView<EntityPaginated<GetAllCategoriesDTO>> resultView = new();
            try
            {
                List<Category> categories = [.. (await catogoryRepository.GetAllAsync()).Where(a => a.IsDeleted != true)
                    .OrderByDescending(c => c.CreatedAt)
                    .Skip((pageNubmer - 1) * pageSize)
                    .Take(pageSize)];

                List<GetAllCategoriesDTO> categoryDTOs = mapper.Map<List<GetAllCategoriesDTO>>(categories);
                int totalCategories = (await catogoryRepository.GetAllAsync()).Count(a => !a.IsDeleted);

                resultView.IsSuccess = true;
                resultView.Data = new EntityPaginated<GetAllCategoriesDTO>
                {
                    Data = categoryDTOs,
                    Count = totalCategories
                };
                resultView.Msg = "All Categories Fetched Successfully";
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happened While Fetching All Categories {ex.Message}";
            }
            return resultView;
        }

    }
}
