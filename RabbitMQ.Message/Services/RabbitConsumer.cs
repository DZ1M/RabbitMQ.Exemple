using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Message.Interfaces;
using System.Text;

namespace RabbitMQ.Message.Services
{
    public class RabbitConsumer: IRabbitConsumer
    {
        private readonly IModel _model;
        private readonly IConnection _connection;
        public RabbitConsumer(IRabbitConnection connection)
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
        public async Task Consumer(string queue)
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (channel, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                await MyFunction(message);

                await Task.CompletedTask;

                _model.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
            };
            _model.BasicConsume(queue, false, consumer);
            await Task.CompletedTask;
        }

        public async Task MyFunction(string request)
        {
            Console.WriteLine(request);
            await Task.Delay(5);
            return;
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
