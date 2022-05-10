using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Infrastructure.Persistence.Context;
using Sozluk.Infrastructure.Persistence.Repositories;

namespace Sozluk.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SozlukContext>(conf =>
        {
            conf.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });
        var seedData = new SeedData();
        seedData.SeedAsync(configuration).GetAwaiter().GetResult();
        //services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IEntryRepository, EntryRepository>();
        services.AddTransient<IEntryCommentRepository, EntryCommentRepository>();
        services.AddTransient<IEmailConfirmationRepository, EmailConfirmationRepository>();
        
        return services;
    }
}