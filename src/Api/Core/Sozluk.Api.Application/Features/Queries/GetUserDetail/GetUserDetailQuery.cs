using MediatR;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetUserDetail;

public class GetUserDetailQuery:IRequest<UserDetailViewModel>
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public GetUserDetailQuery(Guid userId, string userName=null)
    {
        UserId = userId;
        UserName = userName;
    }
}