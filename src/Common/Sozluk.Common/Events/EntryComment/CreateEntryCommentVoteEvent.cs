using Sozluk.Common.ViewModels;

namespace Sozluk.Common.Events.EntryComment;

public class CreateEntryCommentVoteEvent
{
    public Guid EntryCommentId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedBy { get; set; }
}