using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Api.Application.Features.Command.Entry.CreateFav;

public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
{
    public async Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName, exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.CreateEntryFavQueueName,
            obj:new CreateEntryFavEvent()
            {
                EntryId = request.EntryId.Value,
                CreatedBy = request.UserId.Value
            }
        );
        return await Task.FromResult(true);
    }
}