namespace Application.URLs.Dtos;

public class UrlDetailedDto
{
    public Guid Id { get; set; }
    
    public string OriginalUrl { get; set; }
    
    public string ShortUrl { get; set; }
    
    public DateTime CreatedAt { get; set; }
}
