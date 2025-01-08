using Handmade.Application.Contracts;
using Handmade.Context;
using Handmade.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Handmade.Infrastructure
{
    public class GenericRepository<TEntity, TId>(HandmadeContext context) : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly DbSet<TEntity> _dbset = context.Set<TEntity>();

        // we used primary constructor instead of the reqular one like below
        //public GenericRepository(HandmadeContext context)
        //{
        //    _context = context;
        //    _dbset = context.Set<TEntity>();
        //}

        public async Task<TEntity> CreateAsync(TEntity Entity) => (await _dbset.AddAsync(Entity)).Entity;

        public Task<TEntity> DeleteAsync(TEntity Entity) => Task.FromResult(_dbset.Remove(Entity).Entity);

        public Task<IQueryable<TEntity>> GetAllAsync() => Task.FromResult(_dbset.AsQueryable());

        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();

        public Task<TEntity> UpdateAsync(TEntity Entity) => Task.FromResult(_dbset.Update(Entity).Entity);
    }
}
