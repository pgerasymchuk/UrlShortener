using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests.RepositoryTests;

public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
{
    public DbSet<Url> Urls { get; set; } = null!;
}
