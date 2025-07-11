using Application.Interfaces;
using Application.URLs.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.URLs.Commands;

public record CreateShortenedUrlCommand(string OriginalUrl) 
    : IRequest<UrlDetailedDto>;
    
public class CreateShortenedUrlCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<CreateShortenedUrlCommand, UrlDetailedDto>
{
    private static readonly char[] _allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private static readonly int _codeLength = 6;

    public async Task<UrlDetailedDto> Handle(CreateShortenedUrlCommand request, CancellationToken cancellationToken)
    {
        var entity = new Url
        {
            OriginalUrl = request.OriginalUrl,
            ShortCode = await GenerateShortCodeAsync(),
            UserId = currentUserService.UserId,
        };

        await unitOfWork.UrlRepository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();

        var dto = mapper.Map<UrlDetailedDto>(entity);
        return dto;
    }

    private async Task<string> GenerateShortCodeAsync()
    {
        string code;
        do
        {
            var rng = new Random();
            var chars = new char[_codeLength];
            for (var i = 0; i < _codeLength; i++)
            {
                chars[i] = _allowedChars[rng.Next(_allowedChars.Length)];
            }
            
            code = new string(chars);
        } 
        while (await unitOfWork.UrlRepository.DoesShortCodeExistAsync(code));

        return code;
    }
}
