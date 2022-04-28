using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Infrastructure.Persistence.EntityConfiguration.Entry;

public class EntryVoteEntityConfiguration:BaseEntityConfiguration<EntryVote>
{
    public override void Configure(EntityTypeBuilder<EntryVote> builder)
    {
        base.Configure(builder);
        builder.ToTable("entryvote", SozlukContext.DEFAULT_SCHEMA);
        builder.HasOne<Api.Domain.Models.Entry>().WithMany(i=>i.EntryVotes).HasForeignKey(i => i.EntryId);
    }
}