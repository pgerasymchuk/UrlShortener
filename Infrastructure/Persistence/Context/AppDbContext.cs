using Application.Identity;
using Domain.Entities;
using Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Url> ShortenedUrls { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Url>()
            .HasIndex(x => x.ShortCode)
            .IsUnique();

        builder.Entity<Url>()
            .HasQueryFilter(x => x.DeletedAt == null);
        
        builder.SeedUsers();
    }
}