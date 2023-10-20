using RabbitMQ.Client;

namespace RabbitMQ.Message.Interfaces
{
    public interface IRabbitConnection
    {
        IConnection Configure();
    }
}
