using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Message.Interfaces;
using System.Text.Json;

namespace RabbitMQ.Sender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {

        private readonly ILogger<QueueController> _logger;
        private readonly IRabbitSender _senderToRabbit;
        private readonly IConfiguration _configuration;

        public QueueController(ILogger<QueueController> logger, IRabbitSender senderToRabbit, IConfiguration configuration)
        {
            _logger = logger;
            _senderToRabbit = senderToRabbit;
            _configuration = configuration;
        }

        [HttpGet(Name = "SendQueue")]
        public async Task<IActionResult> Test()
        {
            var queue = _configuration["RabbitMqConfig:Queue"];

            _senderToRabbit.DefineQueue(queue);

            for (var i = 0; i < 1000; i++)
            {
                _senderToRabbit.Sender(queue, JsonSerializer.Serialize(new { nome = "Richard", id = Guid.NewGuid()}));

            }
            _logger.LogInformation("Queues Sended!");
            return Ok();
        }
    }
}