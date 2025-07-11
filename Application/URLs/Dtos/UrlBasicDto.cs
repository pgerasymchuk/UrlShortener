namespace Application.URLs.Dtos;

public class UrlBasicDto
{
    public Guid Id { get; set; }
    
    public string OriginalUrl { get; set; }
    
    public string ShortUrl { get; set; }
}