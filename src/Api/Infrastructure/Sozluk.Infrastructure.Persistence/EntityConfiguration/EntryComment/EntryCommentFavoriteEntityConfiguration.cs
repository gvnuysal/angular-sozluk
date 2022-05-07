using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Infrastructure.Persistence.EntityConfiguration.EntryComment;

public class EntryCommentFavoriteEntityConfiguration:BaseEntityConfiguration<EntryCommentFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("entrycommentfavorite", SozlukContext.DEFAULT_SCHEMA);
        
        builder.HasOne(x => x.EntryComment)
            .WithMany(x => x.EntryCommentFavorites)
            .HasForeignKey(x => x.EntryCommentId).OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.CreatedUser)
            .WithMany(x => x.EntryCommentFavorites)
            .HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);
    }
}