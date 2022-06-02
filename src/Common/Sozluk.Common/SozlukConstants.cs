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
   
    public const string FavExchangeName = "FavExchangeName";
    public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueueName";
 

    #endregion
}