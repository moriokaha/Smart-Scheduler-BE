namespace SmartScheduler.Repositories.Contracts
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteByIdAsync(int entityId);
        Task<TEntity> GetByIdAsync(int entityId);
        Task<List<TEntity>> GetAllAsync();
    }
}
