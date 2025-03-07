using Handmade.Application.Contracts;
using Handmade.Context;
using Handmade.Models.ProductH;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Infrastructure
{
    class ProductImageRepository : IProductImageRepository
    {
        private readonly HandmadeContext _context;


        protected readonly DbSet<ProductImage> _dbSet;


        public ProductImageRepository(HandmadeContext context )
        {
            _context = context;
            _dbSet = _context.Set<ProductImage>();

        }
        public async Task<ProductImage> CreateAsync(ProductImage productImage)
        {
              await _dbSet.AddAsync(productImage);
            await _context.SaveChangesAsync();
            return productImage;
        }

        public async Task<ProductImage> UpdateAsync (ProductImage productImage)
        {
            _dbSet.Update(productImage);
            await _context.SaveChangesAsync();
            return productImage;
        }

        public async Task<ProductImage> DeleteAsync(ProductImage entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IQueryable<ProductImage>> GetAllAsync()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<ProductImage> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

     

        public Task<int> SaveChangesAsync()
        {
            return  _context.SaveChangesAsync();

        }
    }
}
