using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Infrastructure.Persistence.EntityConfiguration.EntryComment;

public class EntryCommentEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryComment>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryComment> builder)
    {
        base.Configure(builder);
        builder.ToTable("entrycomment", SozlukContext.DEFAULT_SCHEMA);
        builder.HasOne(i => i.CreatedBy).WithMany(i => i.EntryComments).HasForeignKey(i => i.CreatedById);
        builder.HasOne(i => i.Entry).WithMany(i => i.EntryComments).HasForeignKey(i => i.EntryId);
    }
}