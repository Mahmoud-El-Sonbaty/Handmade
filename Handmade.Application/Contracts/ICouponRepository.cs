using Handmade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Contracts
{
    public interface ICouponRepository : IGenericRepository<Coupon , int>
    {
       public  Task<Coupon> GetByCodeAsync(string couponCode);

        public Task<Coupon> GetByID(int id);


    }
}
