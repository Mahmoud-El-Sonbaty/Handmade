using AutoMapper;
using Handmade.Application.Contracts;
using Handmade.DTOs.CategoryDTOs;
using Handmade.DTOs.ProductReviewDTOs;
using Handmade.DTOs.SharedDTOs;
using Handmade.Models;

namespace Handmade.Application.Services.CategoryServices
{
    public class CategoryService(ICatogoryRepository CatogoryRepository, IMapper _mapper) : ICategoryService
    {
        private readonly ICatogoryRepository catogoryRepository = CatogoryRepository;
        private readonly IMapper mapper = _mapper;

       


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

        }

        public Task<ResultView<GetOneCategoryDTO>> GetOneCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<EntityPaginated<GetAllCategoriesDTO>>> GetPaginatedAsync(int pageNubmer, int pageSize)
        {
            throw new NotImplementedException();
        }

    }
}
