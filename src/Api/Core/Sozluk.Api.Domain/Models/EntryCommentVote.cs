using Sozluk.Common.ViewModels;

namespace Sozluk.Api.Domain.Models;

public class EntryCommentVote:BaseEntity
{
    public Guid EntryCommentId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedById { get; set; }
    public virtual EntryComment EntryComment { get; set; }
}