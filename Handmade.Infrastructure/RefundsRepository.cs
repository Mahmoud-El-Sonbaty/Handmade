using Handmade.Application.Contracts;
using Handmade.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Infrastructure
{
    public class RefundsRepository : IRefundsRepository
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<Refund> _dbset;

        public RefundsRepository( DbContext dbContext )
        {
            _dbContext = dbContext;
            _dbset = _dbContext.Set<Refund>();
        }
        public async Task<Refund> CreateAsync(Refund Entity)
        {
             await _dbset.AddAsync(Entity);
            await _dbContext.SaveChangesAsync();
            return Entity;
        }

        public async Task<Refund> DeleteAsync(Refund Entity)
        {
             _dbset.Remove(Entity);
            await _dbContext.SaveChangesAsync();
            return Entity;
            
        }
        public async Task<Refund> UpdateAsync(Refund Entity)
        {
             _dbset.Update(Entity);
            await _dbContext.SaveChangesAsync();
            return Entity;
        }
        public async Task<IQueryable<Refund>> GetAllAsync()
        {
            return _dbset.AsQueryable();
        }

        public async Task<Refund> GetRefundByIdAsync(int id)
        {
           var result= await _dbset.FindAsync(id);
            return result;
        }

        public async Task<ICollection<Refund>> GetRefundsByOrderIdAsync(int orderId)
        {
            return await _dbset
                .Where(r => r.OrderId == orderId)
                .Include(r => r.User)    
                .Include(r => r.Product) 
                .ToListAsync();
        }

        public async Task<ICollection<Refund>> GetRefundsByUserIdAsync(int userId)
        {
            return await _dbset
                .Where(r => r.UserId == userId)
                .Include(r => r.Product)     
                .ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

      
    }
}
