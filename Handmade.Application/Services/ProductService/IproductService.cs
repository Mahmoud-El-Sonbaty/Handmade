using Handmade.DTOs.ProductDTOs;
using Handmade.DTOs.ProductReviewDTOs;
using Handmade.DTOs.SharedDTOs;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.Products
{
    public interface IproductService 
    {
        Task<ResultView<CRUDProductDTOs>> CreateProductAsync(CRUDProductDTOs entity);
        Task<ResultView<CRUDProductDTOs>> DeleteProductAsync(CRUDProductDTOs entity);
        Task<ResultView<ICollection<GetAllProductsDTOs>>> GetAllProductAsync();
        Task<ResultView<ICollection<CRUDProductDTOs>>> GetByNameAsync(string name);
        Task<ResultView<ICollection<GetAllProductsDTOs>>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<ResultView<CRUDProductDTOs>> UpdateProductAsync(CRUDProductDTOs entity);
        Task<ResultView<GetOneProductDTOs>> GetProductByIdAsync(int id); // resultView is wrapper class
        Task<ResultView<EntityPaginated<GetOneProductDTOs>>> GetPaginatedAsync(int pageNubmer, int pageSize);


    }

}
