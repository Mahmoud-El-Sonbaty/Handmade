using Handmade.DTOs.PaymentMethodDTOs;
using Handmade.DTOs.ProductCategoryDTO;
using Handmade.DTOs.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.PaymentMethodServices
{
    public interface IPaymentMethodService
    {
        public Task<ResultView<CRUDPaymetMethodDTO>> CreateAsync(CRUDPaymetMethodDTO Entity);
        public Task<ResultView<CRUDPaymetMethodDTO>> UpdateAsync(CRUDPaymetMethodDTO Entity);
        public Task<ResultView<CRUDPaymetMethodDTO>> HeardDeleteAsync(CRUDPaymetMethodDTO Entity);
        public Task<ResultView<CRUDPaymetMethodDTO>> SoftDeleteAsync(CRUDPaymetMethodDTO Entity);
        public Task<ResultView<List<CRUDPaymetMethodDTO>>> GetAllAsync();
        public Task<ResultView<CRUDPaymetMethodDTO>> GetOneAsync(int id);
        Task<ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>> GetPaginatedAsync(int pageNumber, int pageSize, int MethodId);
    }
}
