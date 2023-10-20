namespace RabbitMQ.Message.Interfaces
{
    public interface IRabbitSender
    {
        void DefineQueue(string queue);
        void Sender(string queue, string obj);
    }
}
