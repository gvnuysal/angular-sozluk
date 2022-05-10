using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Infrastructure.Persistence.Repositories;

public class EntryCommentRepository:GenericRepository<EntryComment>,IEntryCommentRepository
{
    public EntryCommentRepository(SozlukContext dbContext) : base(dbContext)
    {
    }
}