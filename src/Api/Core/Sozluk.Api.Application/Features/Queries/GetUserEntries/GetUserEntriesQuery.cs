using MediatR;
using Sozluk.Common.ViewModels.Page;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetUserEntries;

public class GetUserEntriesQuery:BasePagedQuery,IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
{
    public Guid? UserId { get; set; }
    public string UserName { get; set; }
    public GetUserEntriesQuery(Guid? userId, string userName=null,int page=1, int pageSize=10) : base(page, pageSize)
    {
        UserId = userId;
        UserName = userName;
    }
}