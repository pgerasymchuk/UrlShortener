using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRepository<TEntity, in TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task<TEntity?> GetByIdAsync(TKey id);
    
    Task AddAsync(TEntity entity);
    
    Task DeleteByIdAsync(TKey id);
}