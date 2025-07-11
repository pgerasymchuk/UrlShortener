using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUrlRepository : IRepository<Url, Guid>
{
    Task<bool> DoesShortCodeExistAsync(string shortCode);
    
    Task<IEnumerable<Url>> GetAllByUserIdAsync(Guid userId);
    
    Task<string?> GetOriginalUrlByShortCodeAsync(string shortCode);
}