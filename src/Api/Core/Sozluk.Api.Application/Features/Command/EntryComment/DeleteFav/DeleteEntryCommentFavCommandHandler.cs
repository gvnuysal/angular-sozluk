using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Api.Application.Features.Command.EntryComment.DeleteFav;

public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.DeleteEntryCommentFavQueueName,
            obj: new DeleteEntryCommentFavEvent
            {
                CreatedBy = request.UserId,
                EntryCommentId = request.EntryCommentId
            });

        return await Task.FromResult<bool>(true);
    }
}