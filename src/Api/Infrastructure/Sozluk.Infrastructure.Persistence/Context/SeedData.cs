using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Infrastructure.Persistence.Context;

public class SeedData
{
    private static List<User> GetUsers()
    {
        var result = new Faker<User>("tr")
            .RuleFor(x => x.Id, i => Guid.NewGuid())
            .RuleFor(x => x.CreateDate,
                x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(x => x.FirstName, x => x.Person.FirstName)
            .RuleFor(x => x.LastName, x => x.Person.LastName)
            .RuleFor(x => x.EmailAddress, x => x.Internet.Email())
            .RuleFor(x => x.UserName, x => x.Internet.UserName())
            .RuleFor(x => x.Password, x => PasswordEncrytor.Encrpt(x.Internet.Password()))
            .RuleFor(x => x.EmailConfirmed, x => x.PickRandom(true, false))
            .Generate(1000);

        return result;
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        var dbContextBuilder = new DbContextOptionsBuilder();
        dbContextBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        var context = new SozlukContext(dbContextBuilder.Options);
        if (context.Users.Any())
        {
            await Task.CompletedTask;
            return;
        }
        var users = GetUsers();

        var userIds = users.Select(x => x.Id);
        await context.Users.AddRangeAsync(users);

        var guids = Enumerable.Range(0, 150).Select(x => Guid.NewGuid()).ToList();
        int counter = 0;

        var entries = new Faker<Entry>("tr")
            .RuleFor(x => x.Id,x=> guids[counter++])
            .RuleFor(x => x.CreateDate, x => x.Date.Between(DateTime.Now.AddDays(-50), DateTime.Now))
            .RuleFor(x => x.Subject, x => x.Lorem.Sentence(10, 10))
            .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
            .RuleFor(x => x.CreatedById, x => x.PickRandom(userIds))
            .Generate(150);
        await context.Entries.AddRangeAsync(entries);

        var entryComments = new Faker<EntryComment>("tr")
            .RuleFor(x => x.Id, x=>Guid.NewGuid())
            .RuleFor(x => x.CreateDate, x => x.Date.Between(DateTime.Now.AddDays(-50), DateTime.Now))
            .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
            .RuleFor(x => x.CreatedById, x => x.PickRandom(userIds))
            .RuleFor(x => x.EntryId, x => x.PickRandom(guids))
            .Generate(1000);
        await context.EntryComments.AddRangeAsync(entryComments);

        await context.SaveChangesAsync();
    }
}