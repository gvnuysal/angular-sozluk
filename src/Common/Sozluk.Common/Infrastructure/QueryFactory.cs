using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Sozluk.Common.Infrastructure;

public static class QueueFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="exchangeName"></param>
    /// <param name="exchangeType"></param>
    /// <param name="queueName"></param>
    /// <param name="obj"></param>
    public static void SendMessageToExchange(string exchangeName, string exchangeType, string queueName, object obj)
    {
        var channel = CreateBasicConsumer().EnsureExchange(exchangeName, exchangeType).EnsureQueue(queueName, exchangeName).Model;
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));
        channel.BasicPublish(exchangeName,queueName,null,body);
    }

    private static EventingBasicConsumer CreateBasicConsumer()
    {
        var factory = new ConnectionFactory() { HostName = SozlukConstants.RabbitMQHost };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        return new EventingBasicConsumer(channel);
    }

    private static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer, string exchangeName,
        string exchangeType = SozlukConstants.DefaultExchangeType)
    {
        consumer.Model.ExchangeDeclare(exchangeName, exchangeType, false, false);

        return consumer;
    }

    public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer, string queueName,
        string exchangeName)
    {
        consumer.Model.QueueDeclare(queueName, false, false, false, null);
        consumer.Model.QueueBind(queueName, exchangeName, queueName);
        return consumer;
    }
}