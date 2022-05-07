using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Infrastructure.Persistence.EntityConfiguration.EntryComment;

public class EntryCommentVoteEntityConfiguration:BaseEntityConfiguration<EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
    {
        base.Configure(builder);
        builder.ToTable("entrycommentvote", SozlukContext.DEFAULT_SCHEMA);
        builder.HasOne(x => x.EntryComment)
            .WithMany(x => x.EntryCommentVotes)
            .HasForeignKey(x => x.EntryCommentId);
    }
}