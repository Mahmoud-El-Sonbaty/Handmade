using Handmade.DTOs.RefundsDTOs;
using Handmade.DTOs.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.RefundsServices
{
   public interface IRefundService
    {
        Task<ResultView<RefundReadDto>> GetRefundByIdAsync(int id);
        Task<ResultView<ICollection<RefundReadDto>>> GetAllRefundsAsync();
        Task<ResultView<RefundCreateDto>> CreateRefundAsync(RefundCreateDto refundDto);
        Task<bool> DeleteRefundAsync(int id);
        Task<ResultView<ICollection<RefundReadDto>>> GetRefundsByOrderIdAsync(int orderId);
        Task<ResultView<ICollection<RefundReadDto>>> GetRefundsByUserIdAsync(int userId);
    }
}
