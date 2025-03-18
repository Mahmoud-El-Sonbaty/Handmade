using Handmade.Application.Contracts;
using Handmade.Context;
using Microsoft.EntityFrameworkCore;
using Handmade.Models.ProductH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        #region Feilds
        private readonly HandmadeContext _context;

        #endregion

        #region Constructor
        public ProductRepository(HandmadeContext handmadeContext)
        {
            _context = handmadeContext;
        }
        #endregion


      

        public async Task<Product> CreateAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

      

        public async Task<Product> DeleteAsync(Product entity)
        {
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

     

        public async Task<IQueryable<Product>> GetAllAsync()
        {
            return _context.Products.AsQueryable();
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<ICollection<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return  _context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
               
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> GetByID(int id)
        {
          var result =await _context.Products.FindAsync(id);

            return result;
        }




    }





}

