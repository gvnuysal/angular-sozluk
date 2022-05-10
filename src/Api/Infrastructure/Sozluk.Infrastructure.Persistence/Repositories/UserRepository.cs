using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Infrastructure.Persistence.Repositories;

public class UserRepository:GenericRepository<User>,IUserRepository
{
    public UserRepository(SozlukContext dbContext) : base(dbContext)
    {
    }
}