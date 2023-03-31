namespace LearningRabbitMQ.ProducerWebApi.RabbitMQ
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
