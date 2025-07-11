using Application.URLs.Dtos;
using AutoMapper;
using Domain.Entities;

namespace WebAPI.MappingProfiles;

public class UrlProfile : Profile
{
    public UrlProfile()
    {
        CreateMap<Url, UrlDetailedDto>()
            .ForMember(dest => dest.ShortUrl, opt => opt.MapFrom(src => $"/r/{src.ShortCode}"));
        
        CreateMap<Url, UrlBasicDto>()
            .ForMember(dest => dest.ShortUrl, opt => opt.MapFrom(src => $"/r/{src.ShortCode}"));

    }
}