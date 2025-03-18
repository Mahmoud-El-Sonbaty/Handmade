using Handmade.Application.Contracts;
using Handmade.Context;
using Handmade.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Infrastructure
{
    public class CouponUsageRepository : ICouponUsageRepository
    {
        private readonly HandmadeContext _context;
        private readonly DbSet<CouponUsage> _dbSet;

        public CouponUsageRepository(HandmadeContext context)
        {
            _context = context;
            _dbSet = _context.Set<CouponUsage>();
        }

        public async Task<CouponUsage> CreateAsync(CouponUsage entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CouponUsage> DeleteAsync(CouponUsage entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IQueryable<CouponUsage>> GetAllAsync()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<CouponUsage> GetByID(int id)
        {
            var result = await _dbSet.FindAsync(id);

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<CouponUsage> UpdateAsync(CouponUsage Entity)
        {
            _dbSet.Update(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }

        Task<Coupon> ICouponUsageRepository.GetByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
