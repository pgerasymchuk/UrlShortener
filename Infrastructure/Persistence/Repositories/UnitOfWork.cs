using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork(DbContext context, IUrlRepository urlRepository) : IUnitOfWork
{
    public IUrlRepository UrlRepository { get; } = urlRepository;

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}