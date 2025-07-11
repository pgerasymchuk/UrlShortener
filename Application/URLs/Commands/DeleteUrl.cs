using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.URLs.Commands;

public record DeleteUrlCommand(Guid Id) : IRequest<Unit>;

public class DeleteUrlCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : IRequestHandler<DeleteUrlCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUrlCommand request, CancellationToken cancellationToken)
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

        await unitOfWork.UrlRepository.DeleteByIdAsync(entity.Id);
        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}