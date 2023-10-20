using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;

namespace RabbitMQ.Message
{
    public class RabbitConnection
    {
        private IConnection _connection;
        private IModel _channel;

        public void Configure()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        public void CloseConnection()
        {
            _channel.Close();
            _connection.Close();
        }
        public void DefineQueue(string queue)
        {
            _channel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void Sender(string queue, string obj)
        {
            var body = Encoding.UTF8.GetBytes(obj);

            _channel.BasicPublish("", queue, null, body);
        }

        public void Consumer(string queue)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Processar(message);
                Console.WriteLine("processado apos funcao.");
                _channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
            };
            _channel.BasicConsume(queue, false, consumer);
        }
        public void Processar(string message)
        {
            Console.WriteLine(message);
        }
    }
}