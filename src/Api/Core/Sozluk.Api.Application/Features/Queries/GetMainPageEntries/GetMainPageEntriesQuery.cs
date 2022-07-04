using MediatR;
using Sozluk.Common.ViewModels.Page;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetMainPageEntries;

public class GetMainPageEntriesQuery:BasePagedQuery,IRequest<PagedViewModel<GetEntryDetailViewModel>>
{
    public Guid? UserId { get; set; }
    
    public GetMainPageEntriesQuery(Guid? userId, int page, int pageSize) : base(page, pageSize)
    {
        UserId = userId;
    }
}