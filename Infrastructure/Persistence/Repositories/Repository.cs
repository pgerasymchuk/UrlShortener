using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class Repository<TEntity, TKey>(DbContext context) : IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    
    protected virtual IQueryable<TEntity> IncludeDetails(IQueryable<TEntity> query) => query;
    
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var query = IncludeDetails(_dbSet.AsNoTracking());
        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        var query = IncludeDetails(_dbSet.AsQueryable());
        return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteByIdAsync(TKey id)
    {
        var entity = await _dbSet.FindAsync(id);
        
        if (entity != null)
        {
            entity.DeletedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }
    }
}