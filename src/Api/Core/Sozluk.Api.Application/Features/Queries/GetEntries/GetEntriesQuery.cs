using MediatR;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetEntries;

public class GetEntriesQuery:IRequest<List<GetEntriesViewModel>>
{
    public bool TodaysEntries { get; set; }
    public int Count { get; set; } = 100;
}