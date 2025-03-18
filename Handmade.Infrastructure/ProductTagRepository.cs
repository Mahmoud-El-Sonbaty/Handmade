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
    public class ProductTagRepository : IProductTagRepository
    {
        private readonly HandmadeContext _context;
        private readonly DbSet<ProductTag> _dbSet;

        public ProductTagRepository(HandmadeContext context)
        {
            _context = context;
            _dbSet = _context.Set<ProductTag>();
        }
        public async Task<ProductTag> CreateAsync(ProductTag Entity)
        {
            await _dbSet.AddAsync(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }

        public async Task<ProductTag> DeleteAsync(ProductTag entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IQueryable<ProductTag>> GetAllAsync()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<ProductTag> GetByIdAsync(int id)
        {
            var result =  await _dbSet.FindAsync(id);
            return result;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<ProductTag> UpdateAsync(ProductTag Entity)
        {
            _dbSet.Update(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }
    }
}
