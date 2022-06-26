using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Api.Application.Features.Command.Entry.DeleteFav;

public class DeleteEntryFavCommandHandler:IRequestHandler<DeleteEntryFavCommand,bool>
{
    public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
    {
         QueueFactory.SendMessageToExchange(exchangeName:SozlukConstants.FavExchangeName,exchangeType:SozlukConstants.DefaultExchangeType,queueName:SozlukConstants.DeleteEntryFavQueueName,
             obj:new DeleteEntryFavEvent
             {
                 CreatedBy = request.UserId,
                 EntryId = request.EntryId
             });
         return await Task.FromResult<bool>(true);
    }
}