using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UrlRepository(DbContext context) : Repository<Url, Guid>(context), IUrlRepository
{
    public async Task<bool> DoesShortCodeExistAsync(string shortCode)
    {
        return await _dbSet.AnyAsync(u => u.ShortCode == shortCode);
    }

    public async Task<IEnumerable<Url>> GetAllByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(u => u.UserId == userId)
            .ToListAsync();
    }

    public async Task<string?> GetOriginalUrlByShortCodeAsync(string shortCode)
    {
        return await _dbSet
            .Where(u => u.ShortCode == shortCode)
            .Select(u => u.OriginalUrl)
            .FirstOrDefaultAsync();
    }
}