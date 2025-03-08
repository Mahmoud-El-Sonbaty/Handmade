namespace Handmade.Application.Contracts
{
    public interface IGenericRepository<TEntity, TId>
    {
        // here we need to discuss about the TId if it is necessary or not based on the how will we implement the GetOneAsync method
        // (do it in the repo with find or do it in the services using the GetAllAsync from the repo then use first or default in the services)
        public Task<TEntity> CreateAsync(TEntity Entity);
        public Task<TEntity> UpdateAsync(TEntity Entity);
        public Task<TEntity> DeleteAsync(TEntity Entity);
        public Task<IQueryable<TEntity>> GetAllAsync();

        public Task<int> SaveChangesAsync();
    }
}
