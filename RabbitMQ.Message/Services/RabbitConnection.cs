using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Message.Config;
using RabbitMQ.Message.Interfaces;

namespace RabbitMQ.Message.Services
{
    public class RabbitConnection : IRabbitConnection
    {
        private readonly RabbitMqConfig _configuration;
        public RabbitConnection(IOptions<RabbitMqConfig> configuration)
        {
            _configuration = configuration.Value;
        }

        public IConnection Configure()
        {
            var factory = new ConnectionFactory
            {
                UserName = _configuration.Username,
                Password = _configuration.Password,
                HostName = _configuration.HostName,
            };

            factory.DispatchConsumersAsync = true;
            var channel = factory.CreateConnection();
            return channel;
        }
    }
}