using Handmade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Contracts
{
    public interface ICouponUsageRepository : IGenericRepository<CouponUsage,int>
    {
        public Task<Coupon> GetByID(int id);

    }
}
