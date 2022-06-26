using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Api.Application.Features.Command.Entry.DeleteVote;

public class DeleteVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.DeleteEntryVoteQueueName,
            obj: new DeleteVoteEntryEvent
            {
                CreatedBy = request.UserId,
                EntryId = request.EntryId
            });
        return await Task.FromResult<bool>(true);
    }
}