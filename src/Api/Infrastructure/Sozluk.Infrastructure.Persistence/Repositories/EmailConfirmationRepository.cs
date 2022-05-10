using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Infrastructure.Persistence.Repositories;

public class EmailConfirmationRepository:GenericRepository<EmailConfirmation>,IEmailConfirmationRepository
{
    public EmailConfirmationRepository(SozlukContext dbContext) : base(dbContext)
    {
    }
}