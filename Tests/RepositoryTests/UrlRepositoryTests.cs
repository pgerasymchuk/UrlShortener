using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.RepositoryTests;

public class UrlRepositoryTests
{
    private static TestDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new TestDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUrls()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repo = new UrlRepository(context);

        var urls = new[]
        {
            new Url { Id = Guid.NewGuid(), OriginalUrl = "https://google.com", ShortCode = "abc123" },
            new Url { Id = Guid.NewGuid(), OriginalUrl = "http://google.com", ShortCode = "abc456" }
        };

        await context.Urls.AddRangeAsync(urls);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, u => u.ShortCode == "abc123");
        Assert.Contains(result, u => u.ShortCode == "abc456");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectUrl()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repo = new UrlRepository(context);
        var targetId = Guid.NewGuid();

        var urls = new[]
        {
            new Url { Id = targetId, OriginalUrl = "https://google.com", ShortCode = "abc123" },
            new Url { Id = Guid.NewGuid(), OriginalUrl = "http://google.com", ShortCode = "abc456" }
        };

        await context.Urls.AddRangeAsync(urls);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.GetByIdAsync(targetId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("abc123", result.ShortCode);
        Assert.Equal("https://google.com", result.OriginalUrl);
    }

    [Fact]
    public async Task AddAsync_ShouldAddUrl()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repo = new UrlRepository(context);
        var url = new Url
        {
            OriginalUrl = "https://google.com",
            ShortCode = "abc123",
            UserId = Guid.NewGuid()
        };

        // Act
        await repo.AddAsync(url);
        await context.SaveChangesAsync();

        var result = await context.Urls.FirstOrDefaultAsync(u => u.Id == url.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("abc123", result.ShortCode);
    }
    
    
    [Fact]
    public async Task DeleteByIdAsync_ShouldSoftDelete()
    {
        var context = GetInMemoryDbContext();
        var repo = new UrlRepository(context);
        var id = Guid.NewGuid();

        await context.Urls.AddAsync(new Url
        {
            Id = id,
            OriginalUrl = "https://google.com",
            ShortCode = "abc123"
        });
        await context.SaveChangesAsync();

        await repo.DeleteByIdAsync(id);
        await context.SaveChangesAsync();

        var deleted = await context.Urls.FindAsync(id);
        Assert.NotNull(deleted!.DeletedAt);
    }

    [Fact]
    public async Task DoesShortCodeExistAsync_ShouldReturnTrue_WhenExists()
    {
        var context = GetInMemoryDbContext();
        var repo = new UrlRepository(context);
        var url = new Url
        {
            OriginalUrl = "https://google.com",
            ShortCode = "abc123",
            UserId = Guid.NewGuid()
        };

        await context.Urls.AddAsync(url);
        await context.SaveChangesAsync();

        Assert.True(await repo.DoesShortCodeExistAsync("abc123"));
    }

    [Fact]
    public async Task GetAllByUserIdAsync_ShouldReturnCorrectUrls()
    {
        var context = GetInMemoryDbContext();
        var repo = new UrlRepository(context);
        var userId = Guid.NewGuid();

        await context.Urls.AddRangeAsync(
            new Url { OriginalUrl = "https://google.com", ShortCode = "abc123", UserId = userId },
            new Url { OriginalUrl = "http://google.com", ShortCode = "abc456", UserId = Guid.NewGuid() }
        );
        await context.SaveChangesAsync();

        var urls = await repo.GetAllByUserIdAsync(userId);
        Assert.Single(urls);
        Assert.Equal("abc123", urls.First().ShortCode);
    }

    [Fact]
    public async Task GetOriginalUrlByShortCodeAsync_ShouldReturnCorrectUrl()
    {
        var context = GetInMemoryDbContext();
        var repo = new UrlRepository(context);
        await context.Urls.AddAsync(new Url
        {
            Id = Guid.NewGuid(),
            OriginalUrl = "https://google.com",
            ShortCode = "abc123"
        });
        await context.SaveChangesAsync();

        var result = await repo.GetOriginalUrlByShortCodeAsync("abc123");
        Assert.Equal("https://google.com", result);
    }
}
