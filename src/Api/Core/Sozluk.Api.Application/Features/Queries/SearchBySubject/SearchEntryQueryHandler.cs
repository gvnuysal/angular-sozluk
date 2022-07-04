using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.ViewModels.Queries;

namespace Sozluk.Api.Application.Features.Queries.SearchBySubject;

public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public SearchEntryQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
    {
        var result = _entryRepository.Get(g => EF.Functions.Like(g.Subject, $"{request.SearchText}%"))
            .Select(s => new SearchEntryViewModel()
            {
                Id = s.Id,
                Subject = s.Subject
            });
        return await result.ToListAsync();
    }
}