namespace RabbitMQ.Message.Interfaces
{
    public interface IRabbitConsumer
    {
        void DefineQueue(string queue);
        Task Consumer(string queue);
    }
}
