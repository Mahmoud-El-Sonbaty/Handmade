using Handmade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Contracts
{
    public interface IRefundsRepository : IGenericRepository<Refund , int>
    {
        Task<Refund> GetRefundByIdAsync(int id);
        Task<ICollection<Refund>> GetRefundsByUserIdAsync(int userId);
        Task<ICollection<Refund>> GetRefundsByOrderIdAsync(int orderId);
    }
}
