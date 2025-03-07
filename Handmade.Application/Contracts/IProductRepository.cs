using Handmade.Models.ProductH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Contracts
{
  public interface IProductRepository : IGenericRepository<Product, int>
    {
        Task<Product> GetByNameAsync(string name);
        Task<ICollection<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        public Task<Product> GetByID(int id);

    }
}
