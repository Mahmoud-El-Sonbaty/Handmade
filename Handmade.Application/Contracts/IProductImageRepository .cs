using Handmade.Models.ProductH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Contracts
{
   public interface IProductImageRepository :IGenericRepository<ProductImage, int>
    {
        public  Task<ProductImage> GetByIdAsync(int id);

    }


}
