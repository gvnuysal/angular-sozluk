using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.ViewModels.RequestModels.Entry;

namespace Sozluk.Api.Application.Features.Command.Entry.CreateVote;

public class CreateEntryVoteCommandHandler:IRequestHandler<CreateEntryVoteCommand,bool>
{
    public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
    {
         QueueFactory.SendMessageToExchange(exchangeName:SozlukConstants.VoteExchangeName,exchangeType:SozlukConstants.DefaultExchangeType,
             queueName:SozlukConstants.CreateEntryVoteQueueName,obj:new CreateEntryVoteEvent
             {
                 EntryId = request.EntryId,
                 CreatedBy = request.CreatedBy,
                 VoteType = request.VoteType
             });

         return await Task.FromResult(true);
    }
}