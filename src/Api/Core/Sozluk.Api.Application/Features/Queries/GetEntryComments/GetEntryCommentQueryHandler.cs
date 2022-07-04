using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Infrastructure.Extensions;
using Sozluk.Common.ViewModels;
using Sozluk.Common.ViewModels.Page;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentQueryHandler:IRequestHandler<GetEntryCommentsQuery,PagedViewModel<GetEntryCommentsViewModel>>
{
    private readonly IEntryCommentRepository _entryCommentRepository;
    
    public GetEntryCommentQueryHandler(IEntryCommentRepository entryCommentRepository)
    {
        _entryCommentRepository = entryCommentRepository;
    }
    public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = _entryCommentRepository.AsQueryAble();
        query = query.Include(x => x.EntryCommentFavorites)
            .Include(x => x.CreatedBy)
            .Include(x => x.EntryCommentVotes)
            .Where(x=>x.EntryId==request.EntryId);
        
        var list = query.Select(x => new GetEntryCommentsViewModel()
        {
            Id = x.Id, 
            Content = x.Content,
            IsFavorited = request.UserId.HasValue && x.EntryCommentFavorites.Any(a => a.CreatedById == request.UserId),
            FavoritedCount = x.EntryCommentFavorites.Count,
            CreatedDate = x.CreateDate,
            CreatedByUserName = x.CreatedBy.UserName,
            VoteType = request.UserId.HasValue && x.EntryCommentVotes.Any(c => c.CreatedById == request.UserId)
                ? x.EntryCommentVotes.FirstOrDefault(b => b.CreatedById == request.UserId)!.VoteType:VoteType.None
        });
        var entries = await list.GetPaged(request.Page, request.PageSize);
        
        return entries;
    }
}