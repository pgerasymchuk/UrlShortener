using Application.Common.Exceptions;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.URLs.Queries;

public record GetOriginalUrlByShortCodeQuery(string ShortCode) : IRequest<string>;

public class GetOriginalUrlByShortCodeQueryHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<GetOriginalUrlByShortCodeQuery, string>
{
    public async Task<string> Handle(GetOriginalUrlByShortCodeQuery request, CancellationToken cancellationToken)
    {
        var originalUrl = await unitOfWork.UrlRepository.GetOriginalUrlByShortCodeAsync(request.ShortCode);

        if (originalUrl == null)
        {
            throw new NotFoundException($"No URL found for short code: {request.ShortCode}");
        }

        return originalUrl;
    }
}