namespace Domain.Entities;

public class Url : BaseEntity<Guid>
{
    public string OriginalUrl { get; set; }
    
    public string ShortCode { get; set; }
    
    public Guid? UserId { get; set; }
}