using Handmade.Application.Contracts;
using Handmade.Context;
using Handmade.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Handmade.Infrastructure
{
    public class GenericRepository<TEntity, TId>(HandmadeContext context) : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly DbSet<TEntity> _dbset = context.Set<TEntity>();
        private readonly HandmadeContext _context;

        ////we used primary constructor instead of the reqular one like below
        // public GenericRepository(HandmadeContext context)
        //{
        //    _context = context;
        //    _dbset = context.Set<TEntity>();
        //}

        public async Task<TEntity> CreateAsync(TEntity Entity) => (await _dbset.AddAsync(Entity)).Entity;

        public Task<TEntity> UpdateAsync(TEntity Entity) => Task.FromResult(_dbset.Update(Entity).Entity);

        public Task<TEntity> DeleteAsync(TEntity Entity) => Task.FromResult(_dbset.Remove(Entity).Entity);

        public Task<IQueryable<TEntity>> GetAllAsync() => Task.FromResult(_dbset.AsQueryable());

        public async ValueTask<TEntity> GetOneAsync(TId id)
        {
            var entity = await _dbset.FindAsync(id);
            return entity != null && !entity.IsDeleted ? entity : null;
        }

        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
        public async Task<IQueryable<TEntity>> GetSortedFilterAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, Expression<Func<TEntity, bool>> searchPredicate = null, bool ascending = true)
        {
            var query = _dbset.AsQueryable();
            query = query.Where(p => !p.IsDeleted);
            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }
            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            return await Task.FromResult(query.Any() ? query : Enumerable.Empty<TEntity>().AsQueryable());
        }
    }
}
