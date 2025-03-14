using Handmade.DTOs.ProductDTOs;
using Handmade.DTOs.ProductTagsDTOs;
using Handmade.DTOs.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.ProductTagsService
{
    public interface IProductTagService
    {
        Task<ResultView<CRUDProductTagDTOs>> CreateProductTagAsync(CRUDProductTagDTOs entity);
        Task<ResultView<CRUDProductTagDTOs>> DeleteProductTagAsync(CRUDProductTagDTOs entity);
        Task<ResultView<ICollection<CRUDProductTagDTOs>>> GetAllProductTagsAsync();
        Task<ResultView<ICollection<CRUDProductTagDTOs>>> SearchProductTagByNameAsync(string name);
        Task<ResultView<CRUDProductTagDTOs>> UpdateProductTagsAsync(CRUDProductTagDTOs entity);
        Task<ResultView<CRUDProductTagDTOs>> GetProductTagByIdAsync(int id); // resultView is wrapper class
    }
}
