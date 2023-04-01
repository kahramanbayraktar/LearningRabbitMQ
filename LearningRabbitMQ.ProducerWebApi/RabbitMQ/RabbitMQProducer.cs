using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace LearningRabbitMQ.ProducerWebApi.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // exclusive is set to false, otherwise it causes the below error:
            // RabbitMQ.Client.Exceptions.OperationInterruptedException: The AMQP operation was interrupted: AMQP close-reason, initiated by Peer, code=405, text=’RESOURCE_LOCKED – cannot obtain exclusive access to locked queue in ‘orders’ vhost ‘/’.
            // https://github.com/pardahlman/RawRabbit/issues/192
            channel.QueueDeclare("forecasts", exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "forecasts", body: body);
        }
    }
}
