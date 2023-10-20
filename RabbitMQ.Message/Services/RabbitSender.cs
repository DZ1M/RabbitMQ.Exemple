using RabbitMQ.Client;
using RabbitMQ.Message.Interfaces;
using System.Text;

namespace RabbitMQ.Message.Services
{
    public class RabbitSender : IRabbitSender, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;
        public RabbitSender(IRabbitConnection connection)
        {
            _connection = connection.Configure();
            _model = _connection.CreateModel();
        }
        public void DefineQueue(string queue)
        {
            _model.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
            _model.ExchangeDeclare($"{queue}Exchange", ExchangeType.Fanout, durable: true, autoDelete: false);
            _model.QueueBind(queue, $"{queue}Exchange", string.Empty);
        }
        public void Sender(string queue, string obj)
        {
            var body = Encoding.UTF8.GetBytes(obj);

            _model.BasicPublish($"{queue}Exchange", string.Empty, null, body);
        }
        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
