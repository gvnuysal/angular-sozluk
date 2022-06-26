using MediatR;

namespace Sozluk.Api.Application.Features.Command.EntryComment.DeleteFav;

public class DeleteEntryCommentFavCommand:IRequest<bool>
{
    public Guid EntryCommentId { get; set; }
    public Guid UserId { get; set; }

    public DeleteEntryCommentFavCommand()
    {
        
    }
    public DeleteEntryCommentFavCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }


}