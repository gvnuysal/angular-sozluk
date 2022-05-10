using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Infrastructure.Persistence.Repositories;

public class EntryRepository:GenericRepository<Entry>,IEntryRepository
{
    public EntryRepository(SozlukContext dbContext) : base(dbContext)
    {
    }
}