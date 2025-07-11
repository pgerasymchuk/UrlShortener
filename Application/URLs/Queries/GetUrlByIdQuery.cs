using Application.Common.Exceptions;
using Application.Interfaces;
using Application.URLs.Dtos;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.URLs.Queries;

public record GetUrlByIdQuery(Guid Id) : IRequest<UrlDetailedDto>;

public class GetUrlDetailsQueryHandler(
    IUnitOfWork unitOfWork, 
    ICurrentUserService currentUserService, 
    IMapper mapper)  
    : IRequestHandler<GetUrlByIdQuery, UrlDetailedDto>
{
    public async Task<UrlDetailedDto> Handle(GetUrlByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.UrlRepository.GetByIdAsync(request.Id);
        if (entity == null)
        {
            throw new NotFoundException("URL with the specified id was not found."); 
        }

        if (!currentUserService.IsAdmin && currentUserService.UserId != entity.UserId)
        {
            throw new UnauthorizedException("Users can only delete their own URLs.");
        }
        
        return mapper.Map<UrlDetailedDto>(entity);
    }
}
