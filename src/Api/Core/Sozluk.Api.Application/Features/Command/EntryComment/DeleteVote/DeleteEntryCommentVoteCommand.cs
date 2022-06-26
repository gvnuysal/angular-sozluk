using MediatR;

namespace Sozluk.Api.Application.Features.Command.EntryComment.DeleteVote;

public class DeleteEntryCommentVoteCommand:IRequest<bool>
{
    public Guid EntryCommentId { get; set; }
    public Guid UserId { get; set; }

    public DeleteEntryCommentVoteCommand()
    {
        
    }
    public DeleteEntryCommentVoteCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }
}