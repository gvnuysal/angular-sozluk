using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Api.Application.Features.Command.EntryComment.CreateFav;

public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueryFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName, exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.CreateEntryCommentFavQueueName, obj: new CreateEntryCommentFavEvent()
            {
                CreatedBy = request.UserId,
                EntryCommentId = request.EntryCommentId
            });
        
        return await Task.FromResult(true);
    }
}