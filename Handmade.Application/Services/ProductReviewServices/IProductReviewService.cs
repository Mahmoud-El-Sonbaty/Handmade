using Handmade.DTOs.ProductReviewDTOs;
using Handmade.DTOs.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.ProductReviewServices
{
    public interface IProductReviewService
    {
        Task<ResultView<GCUProductReviewDTO>> CreateProductReviewAsync(GCUProductReviewDTO productReviewDTO);
        Task<ResultView<GCUProductReviewDTO>> UpdateProductReviewAsync(GCUProductReviewDTO productReviewDTO);
        Task<ResultView<GCUProductReviewDTO>> DeleteProductReviewAsync(int id);
        Task<ResultView<List<GCUProductReviewDTO>>> GetAllProductReviewsAsync();
        Task<ResultView<GCUProductReviewDTO>> GetProductReviewByIdAsync(int id);
        Task<ResultView<EntityPaginated<GCUProductReviewDTO>>> GetPaginatedAsync(int pageNubmer, int pageSize);
    }
}
