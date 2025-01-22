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
        public  async Task<ResultView<CRUDCategoriesDTO>> DeleteCategoryAsync(int id,string lang)
        {
            var allCategories = await catogoryRepository.GetAllAsync();

            try
            {
                
                if (id <= 0)
                {
                    return new ResultView<CRUDCategoriesDTO> { IsSuccess = false, 
                        Msg = (lang=="en")? "Wrong Id":"رمز تعريفي خاطئ" };
                }
                else if (!allCategories.Any(cat => cat.Id == id))
                {
                    return new ResultView<CRUDCategoriesDTO>
                    {
                        IsSuccess = false,
                        Msg = (lang == "en") ? "This Category Not Found!." : "لم نجد هذا التصنيف!"
                    };


                }
                else
                {
                    Category? successCategory = (await catogoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == id);
                
                     if (allCategories.Any(c => c.ParentCategoryId == id))
                    {
                        return new ResultView<CRUDCategoriesDTO> { IsSuccess = false, Msg = (lang=="en")? $"Category {successCategory.EnName} Couldn't Be Deleted As There Are Categories That Depend On It": $"تعذر حذف الفئة {successCategory.ArName} نظرًا لوجود فئات تعتمد عليها" };
                    }
                    /*     else if (allCategories.Any(c => c.Id == id && c.Products.Any(p => p.CategoryId == c.Id)))
                         {
                             resultView.IsSuccess = false;
                             resultView.Data = null;
                             resultView.Msg = $"Category {successCategory.NameEn} Couldn't Be Deleted As There Are Products That Depend On It";
                         }*/
                    else
                    {
                        var cat = allCategories.FirstOrDefault(Id => Id.Id == id);
                        await catogoryRepository.DeleteAsync(cat);
                        CRUDCategoriesDTO result = mapper.Map<CRUDCategoriesDTO>(cat);
                        await catogoryRepository.SaveChangesAsync();
                        return new ResultView<CRUDCategoriesDTO> { Data = result, IsSuccess = true, Msg = (lang=="en")? $"Category with Name {result.EnName} is Deleted Succssfully": $"تم حذف الفئة التي تحمل الاسم {result.EnName} بنجاح" };

                    }
                }
               
            }
            catch (Exception ex) { 
            
                return new ResultView<CRUDCategoriesDTO> { IsSuccess = false, Msg = (lang == "en") ? "there is an error! : " : "هناك خطأ؟ :" + ex.Message  };
               
            }
        }


        // Get All & Get one 
        /* public async Task<ResultView<List<GetAllCategoriesDTO>>> GetAllCategoriesAsync()
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
}*/
        public async Task<List<GetAllCategoriesDTO>> nestedCategory(List<Category> categories, int parentId, string lang)
        {
            List<GetAllCategoriesDTO> result = new();
            
            foreach (Category child in categories.Where(c => c.ParentCategoryId == parentId))
            {
                GetAllCategoriesDTO obj = new();
                obj.Id = child.Id;
                obj.Name = (lang == "en") ? child.EnName : child.ArName;
                obj.Level = child.Level;
                obj.IsParent = child.IsParent;
                obj.ParentCategoryId = child.ParentCategoryId;
                obj.CatogorylogoPath = child.CatogorylogoPath;

                if (child.IsParent)
                {
                    obj.Children = await nestedCategory(categories, child.Id, lang);
                }
             
                result.Add(obj);
            }
            return result;
        }





        public async Task<ResultView<List<GetAllCategoriesDTO>>> GetAllCategoriesAsync(string lang)
        {
            try
            {
                List<Category> categories = [.. (await catogoryRepository.GetAllAsync()).Where(a => a.IsDeleted != true)];
                if (categories == null)
                {
                    return new ResultView<List<GetAllCategoriesDTO>> 
                    { IsSuccess = false, 
                        Msg = (lang== "en")?  "Non Categories Found!" : "لا توجد فئات لعرضها " };

                }


                List<GetAllCategoriesDTO> result = new();
                List<Category> grandParents = categories.Where(c => c.ParentCategoryId == 0).ToList();
                foreach (Category parent in grandParents)
                {
                    GetAllCategoriesDTO mappedCat = new();
                    mappedCat.Id = parent.Id;
                    mappedCat.Name = (lang == "en") ? parent.EnName : parent.ArName;
                    mappedCat.Level = parent.Level;
                    mappedCat.IsParent = parent.IsParent;
                    mappedCat.ParentCategoryId = parent.ParentCategoryId;
                    mappedCat.CatogorylogoPath = parent.CatogorylogoPath;

                    if (parent.IsParent)
                    {
                        mappedCat.Children = await nestedCategory(categories , parent.Id, lang);
                    }
                    result.Add(mappedCat);
                }
                return new ResultView<List<GetAllCategoriesDTO>> { Data = result, IsSuccess = true, Msg = (lang == "en") ? "Categories fetched successfully!" : "تم جلب الفئات بنجاح" };
            }
            catch (Exception ex)
            {
                return new ResultView<List<GetAllCategoriesDTO>>
                {
                    IsSuccess = false,
                    Msg = (lang == "en") ? "there is an error while Fetching All Categories! : " : "هناك خطأ اثناء جلب الفئات؟ :" + ex.Message
                };
                }

        }

        public async Task<ResultView<GetOneCategoryDTO>> GetOneCategoryByIdAsync(int id, string lang , bool withChild)
        {

            try
            {
                Category? category = (await catogoryRepository.GetAllAsync()).Where(c => c.Id == id && !c.IsDeleted).FirstOrDefault();
                

                if (category is null) {
                    return new ResultView<GetOneCategoryDTO> { IsSuccess = false, Msg = (lang == "en") ? $"We did not succeed in getting the intended category and its ID : {id} " :
                        $"لم ننجح بجلب الفئة المقصودة والتي عنصرها : {id}" };
                }

                GetOneCategoryDTO getOneCategoryDTO = new()
                {
                    Id= category.Id,
                    ParentCategoryId= category.ParentCategoryId,
                    Name= (lang=="en")? category.EnName :category.ArName,
                    Level= category.Level,
                    IsParent= category.IsParent,
                    CatogorylogoPath= category.CatogorylogoPath,
                    Children=null
                };
                if (withChild && getOneCategoryDTO.IsParent) {

                    List<Category>? cat = [..(await catogoryRepository.GetAllAsync()).Where(c => c.Id == getOneCategoryDTO.Id && !c.IsDeleted)];
                    foreach (Category category1 in cat)
                    {
                        GetOneCategoryDTO nestedDTO1 = new GetOneCategoryDTO()
                        {
                            Id = category1.Id,
                            Level = category1.Level,
                            Name = (lang == "en") ? category1.EnName : category1.ArName,
                            ParentCategoryId = category1.ParentCategoryId,
                            IsParent = category1.IsParent,
                        };
                        getOneCategoryDTO.Children?.Add(nestedDTO1);
                    }
                }

                return new ResultView<GetOneCategoryDTO>
                {
                    IsSuccess = true,

                    Msg = (lang == "en") ? $"All Categories with ID Or parent ID {id} Fetched Successfully" :
                        $"تم جلب جميع الفئات المعرفة ب او والدها معرف ب  {id} بنجاح",
                    Data = getOneCategoryDTO
                };
            }
            catch (Exception ex) {
                return new ResultView<GetOneCategoryDTO> { IsSuccess = false, Msg = (lang == "en") ? "there is an error! : " : "هناك خطأ؟ :" + ex.Message };
            }

        }  
        public async Task<ResultView<List<GetOneCategoryDTO>>> GetTheCategoriesByParentIdAsync(int id, string lang, bool withChild)
        {

            try
            { 
                List<Category> categories = [.. (await catogoryRepository.GetAllAsync()).Where(c => c.ParentCategoryId == id && c.IsDeleted != true)];


                if (categories is null)
                {
                    return new ResultView<List<GetOneCategoryDTO>>
                    {
                        IsSuccess = false,
                        Msg = (lang == "en") ? $"We did not succeed in getting the intended category and its ID : {id} " :
                        $"لم ننجح بجلب الفئة المقصودة والتي عنصرها : {id}"
                    };
                }

                List<GetOneCategoryDTO> childs= new ();
                foreach (var category in categories)
                {
                    GetOneCategoryDTO getOneCategoryDTO = new()
                    {
                        Id = category.Id,
                        ParentCategoryId = category.ParentCategoryId,
                        Name = (lang == "en") ? category.EnName : category.ArName,
                        Level = category.Level,
                        IsParent = category.IsParent,
                        CatogorylogoPath = category.CatogorylogoPath,
                        Children = null
                    };
                    if (withChild && getOneCategoryDTO.IsParent)
                    {

                        List<Category>? cat = [.. (await catogoryRepository.GetAllAsync()).Where(c => c.ParentCategoryId == getOneCategoryDTO.Id && !c.IsDeleted)];
                        foreach (Category category1 in cat)
                        {
                            GetOneCategoryDTO nestedDTO1 = new GetOneCategoryDTO()
                            {
                                Id = category1.Id,
                                Level = category1.Level,
                                Name = (lang == "en") ? category1.EnName : category1.ArName,
                                ParentCategoryId = category1.ParentCategoryId,
                                IsParent = category1.IsParent,
                            };
                            getOneCategoryDTO.Children?.Add(nestedDTO1);
                        }
                    }

                    childs.Add(getOneCategoryDTO);  
                }
                return new ResultView<List<GetOneCategoryDTO>>
                {
                    IsSuccess = true,
                    Msg = (lang == "en") ? $"All Categories with ID Or parent ID {id} Fetched Successfully" :
                        $"تم جلب جميع الفئات المعرفة ب او والدها معرف ب  {id} بنجاح",
                    Data = childs
                };
            }
            catch (Exception ex) {
                return new ResultView<List<GetOneCategoryDTO>> { IsSuccess = false, Msg = (lang == "en") ? "there is an error! : " : "هناك خطأ؟ :" + ex.Message };
            }

        }

        public async Task<ResultView<EntityPaginated<GetAllCategoriesDTO>>> GetPaginatedAsync(int pageNubmer, int pageSize, string lang)
        {
            try
            {
                List<Category> categories = [.. (await catogoryRepository.GetAllAsync()).Where(a => a.IsDeleted != true)
            .OrderByDescending(c => c.CreatedAt)
            .Skip((pageNubmer - 1) * pageSize)
            .Take(pageSize)];

                if (categories is null)
                {
                    return new ResultView<EntityPaginated<GetAllCategoriesDTO>>
                    {
                        IsSuccess = false,
                        Msg = (lang == "en") ? "Non Categories Found!" : "لا توجد فئات لعرضها ",
                        Data = new EntityPaginated<GetAllCategoriesDTO>
                        {
                            Data=null,
                            Count=0,
                        }
                    };

                }
                List<GetAllCategoriesDTO> categoryDTOs = new();
                List<Category> grandParents = categories.Where(c => c.ParentCategoryId == 0).ToList();
                foreach (Category parent in grandParents)
                {
                    GetAllCategoriesDTO mappedCat = new();
                    mappedCat.Id = parent.Id;
                    mappedCat.Name = (lang == "en") ? parent.EnName : parent.ArName;
                    mappedCat.Level = parent.Level;
                    mappedCat.IsParent = parent.IsParent;
                    mappedCat.ParentCategoryId = parent.ParentCategoryId;
                    mappedCat.CatogorylogoPath = parent.CatogorylogoPath;

                    if (parent.IsParent)
                    {
                        mappedCat.Children = await nestedCategory(categories , parent.Id, lang);
                    }
                    categoryDTOs.Add(mappedCat);
                }
                int totalCategories = (await catogoryRepository.GetAllAsync()).Count(a => !a.IsDeleted);

               
                return new ResultView<EntityPaginated<GetAllCategoriesDTO>>{
                    Msg = (lang == "en") ? "All Categories Fetched Successfully":"تم جلب جميع الفئات",
                    Data = new EntityPaginated<GetAllCategoriesDTO>
                    {
                        Data = categoryDTOs,
                        Count = totalCategories
                    },
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
              
                return new ResultView<EntityPaginated<GetAllCategoriesDTO>>
                {
                     Msg = (lang == "en") ? "there is an error while Fetching All Categories! : " : "هناك خطأ اثناء جلب الفئات؟ :"+   ex.Message ,
                    Data = new EntityPaginated<GetAllCategoriesDTO>
                    {
                        Data = null,
                        Count = 0
                    },
                    IsSuccess = false
                };
            }
            
        }


    }
}
