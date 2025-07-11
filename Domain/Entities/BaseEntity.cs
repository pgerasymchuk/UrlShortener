namespace Domain.Entities;

public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? DeletedAt { get; set; }
}