using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Infrastructure.Extensions;
using Sozluk.Common.ViewModels.Page;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetUserEntries;

public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryAble();
        if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
        {
            query = query.Where(w => w.CreatedById == request.UserId);
        }
        else if (!string.IsNullOrEmpty(request.UserName))
        {
            query = query.Where(w => w.CreatedBy.UserName == request.UserName);
        }
        else
        {
            return null;
        }

        query = query.Include(i => i.EntryFavorites)
            .Include(i => i.CreatedBy);
        var list = query.Select(s => new GetUserEntriesDetailViewModel()
        {
            Id = s.Id,
            Subject = s.Subject,
            Content = s.Content,
            IsFavorited = false,
            FavoritedCount = s.EntryFavorites.Count,
            CreatedByUserName = s.CreatedBy.UserName
        });
        var entries = await list.GetPaged(request.Page, request.PageSize);
        return entries;
    }
}