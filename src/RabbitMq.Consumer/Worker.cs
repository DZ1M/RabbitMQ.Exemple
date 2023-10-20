using Microsoft.Extensions.Options;
using RabbitMQ.Message.Interfaces;

namespace RabbitMq.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitConsumer _consumer;
        private readonly IConfiguration _configuration;
        public Worker(ILogger<Worker> logger, IRabbitConsumer consumer, IConfiguration configuration)
        {
            _logger = logger;
            _consumer = consumer;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queue = _configuration["RabbitMqConfig:Queue"];
            _consumer.DefineQueue(queue);
            await _consumer.Consumer(queue);
        }
    }
}