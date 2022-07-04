using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Infrastructure.Extensions;
using Sozluk.Common.ViewModels;
using Sozluk.Common.ViewModels.Page;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetMainPageEntries;

public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryAble();
        query = query.Include(x => x.EntryFavorites)
            .Include(x => x.CreatedBy)
            .Include(x => x.EntryVotes);
        var list = query.Select(x => new GetEntryDetailViewModel()
        {
            Id = x.Id,
            Subject = x.Subject,
            Content = x.Content,
            IsFavorited = request.UserId.HasValue && x.EntryFavorites.Any(a => a.CreatedById == request.UserId),
            FavoritedCount = x.EntryFavorites.Count,
            CreatedDate = x.CreateDate,
            CreatedByUserName = x.CreatedBy.UserName,
            VoteType = request.UserId.HasValue && x.EntryVotes.Any(c => c.CreatedById == request.UserId)
                ? x.EntryVotes.FirstOrDefault(b => b.CreatedById == request.UserId)!.VoteType:VoteType.None
        });
        var entries = await list.GetPaged(request.Page, request.PageSize);
        return entries;
    }
}