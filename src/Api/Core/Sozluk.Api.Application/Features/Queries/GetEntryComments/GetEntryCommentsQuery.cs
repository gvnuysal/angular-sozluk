using MediatR;
using Sozluk.Common.ViewModels.Page;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentsQuery:BasePagedQuery,IRequest<PagedViewModel<GetEntryCommentsViewModel>>
{
    public Guid? EntryId { get; set; }
    public Guid? UserId { get; set; }
    public GetEntryCommentsQuery(Guid? entryId, Guid? userId,int page, int pageSize) : base(page, pageSize)
    {
        EntryId = entryId;
        UserId = userId;
    }
}