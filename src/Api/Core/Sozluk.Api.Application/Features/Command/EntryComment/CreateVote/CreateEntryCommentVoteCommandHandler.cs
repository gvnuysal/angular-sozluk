using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.ViewModels.RequestModels.Entry;

namespace Sozluk.Api.Application.Features.Command.EntryComment.CreateVote;

public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.CreateEntryCommentVoteQueueName,
            obj: new CreateEntryCommentVoteEvent
            {
                CreatedBy = request.CreatedBy,
                VoteType = request.VoteType,
                EntryCommentId = request.EntryCommentId
            });
        return await Task.FromResult(true);
    }
}