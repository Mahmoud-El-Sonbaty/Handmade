using Handmade.Application.Contracts;
using Handmade.Context;
using Handmade.Models;
using Handmade.Models.ProductH;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Infrastructure
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HandmadeContext _context;
        private readonly DbSet<Coupon> _dbSet;

        public CouponRepository(HandmadeContext context)
        {
            _context = context;
            _dbSet = _context.Set<Coupon>();
        }

        public async Task<Coupon> CreateAsync(Coupon entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Coupon> DeleteAsync(Coupon entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IQueryable<Coupon>> GetAllAsync()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<Coupon> GetByCodeAsync(string couponCode)
        {
          return  await _dbSet.FirstOrDefaultAsync(a => a.CouponCode == couponCode) ;
        }

        public async Task<Coupon> GetByID(int id)
        {
            var result = await _context.Coupons.FindAsync(id);

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
          return  await _context.SaveChangesAsync();
        }

        public async Task<Coupon> UpdateAsync(Coupon Entity)
        {
            _context.Coupons.Update(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }
    }
}
