namespace Sozluk.Common;

public class SozlukConstants
{
    public const string RabbitMQHost = "localhost";
    public const string DefaultExchangeType = "direct";

    #region User

    public const string UserExchangeName = "UserExchange";
    public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

    #endregion

    #region Fav

    public const string FavExchangeName = "FavExchange";
    public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
    public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
    public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";
    public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";
    public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVote";

    #endregion

    #region Vote

    public const string VoteExchangeName = "VoteExchange";
    public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";
    public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";
    public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVote";

    #endregion
}