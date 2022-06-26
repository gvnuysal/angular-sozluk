using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Api.Application.Features.Command.EntryComment.DeleteVote;

public class DeleteEntryCommentVoteCommandHandler:IRequestHandler<DeleteEntryCommentVoteCommand,bool>
{
    public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.DeleteEntryCommentVoteQueueName,
            obj: new DeleteEntryCommentVoteEvent
            {
                CreatedBy = request.UserId,
                EntryCommentId = request.EntryCommentId
            });

        return await Task.FromResult<bool>(true);
    }
}