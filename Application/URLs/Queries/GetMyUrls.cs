using Application.Common.Exceptions;
using Application.Interfaces;
using Application.URLs.Dtos;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.URLs.Queries;

public record GetMyUrlsQuery : IRequest<IEnumerable<UrlBasicDto>>;

public class GetMyUrlsQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<GetMyUrlsQuery, IEnumerable<UrlBasicDto>>
{
    public async Task<IEnumerable<UrlBasicDto>> Handle(GetMyUrlsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = currentUserService.UserId 
            ?? throw new UnauthenticatedException("Only authenticated users can retrieve their URLs.");
        
        var urls = await unitOfWork.UrlRepository.GetAllByUserIdAsync(currentUserId);
        return mapper.Map<List<UrlBasicDto>>(urls);
    }
}