namespace Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUrlRepository UrlRepository { get; }
    
    Task SaveChangesAsync();
}