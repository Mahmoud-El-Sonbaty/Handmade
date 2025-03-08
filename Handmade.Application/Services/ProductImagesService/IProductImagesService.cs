using Handmade.DTOs.ProductDTOs;
using Handmade.DTOs.ProductImagesDTOs;
using Handmade.DTOs.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.ProductImages
{
    public interface IProductImagesService
    {
        Task<ResultView<ProductImageDTO>> CreateProductImageAsync(ProductImageDTO entity);
        Task<ResultView<ProductImageDTO>> DeleteProductImageAsync(ProductImageDTO entity);
        Task<ResultView<ICollection<ProductImageDTO>>> GetAllProductImagesAsync();
        Task<ResultView<ProductImageDTO>> UpdateProductImageAsync(ProductImageDTO entity);
        Task<ResultView<ProductImageDTO>> GetProductImageByIdAsync(int id); // resultView is wrapper class
    }
}
