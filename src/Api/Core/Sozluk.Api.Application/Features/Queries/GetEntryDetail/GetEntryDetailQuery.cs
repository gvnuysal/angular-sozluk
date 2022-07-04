using MediatR;
using Sozluk.Common.ViewModels.Page;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetEntryDetail;

public class GetEntryDetailQuery:IRequest<GetEntryDetailViewModel>
{
    public Guid EntryId { get; set; }
    public Guid? UserId { get; set; }
    public GetEntryDetailQuery(Guid entryId, Guid? userId)
    {
        EntryId = entryId;
        UserId = userId;
    }
}