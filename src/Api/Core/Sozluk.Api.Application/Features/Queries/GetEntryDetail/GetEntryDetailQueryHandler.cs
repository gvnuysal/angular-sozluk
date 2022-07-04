using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetEntryDetail;

public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
{
    private readonly IEntryRepository _entryRepository;

    public GetEntryDetailQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryAble();
        query = query.Include(i => i.EntryFavorites)
            .Include(i => i.CreatedBy)
            .Include(i => i.EntryVotes)
            .Where(w => w.CreatedById == request.EntryId);
        var list = query.Select(s => new GetEntryDetailViewModel()
        {
            Id = s.Id,
            Subject = s.Subject,
            Content = s.Content,
            IsFavorited = request.UserId.HasValue && s.EntryFavorites.Any(a => a.CreatedById == request.UserId),
            FavoritedCount = s.EntryFavorites.Count,
            CreatedDate = s.CreateDate,
            CreatedByUserName = s.CreatedBy.UserName,
            VoteType = request.UserId.HasValue && s.EntryVotes.Any(a => a.CreatedById == request.UserId)
                ? s.EntryVotes.FirstOrDefault(f => f.CreatedById == request.UserId)!.VoteType
                : Common.ViewModels.VoteType.None
        });
        return await list.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}