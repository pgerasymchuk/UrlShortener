using Application.Interfaces;
using Application.URLs.Dtos;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.URLs.Queries;

public record GetAllUrlsQuery : IRequest<IEnumerable<UrlBasicDto>>;

public class GetAllUrlsQueryHandler(
    IUnitOfWork unitOfWork, 
    ICurrentUserService currentUserService, 
    IMapper mapper) 
    : IRequestHandler<GetAllUrlsQuery, IEnumerable<UrlBasicDto>>
{
    public async Task<IEnumerable<UrlBasicDto>> Handle(GetAllUrlsQuery request, CancellationToken cancellationToken)
    {
        if (currentUserService.IsAdmin == false)
        {
            throw new UnauthorizedAccessException("Only admins can retrieve all URLs.");
        }
        
        var urls = await unitOfWork.UrlRepository.GetAllAsync();
        return mapper.Map<IEnumerable<UrlBasicDto>>(urls);
    }
}
