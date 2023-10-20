using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Message;
using System.Text.Json;
using System.Threading.Channels;

namespace RabbitMQ.Sender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private IConnection _connection;
        private IModel _channel;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var queue = "apenas-teste";

            var configureRabbit = new RabbitConnection();
            configureRabbit.Configure();
            configureRabbit.DefineQueue(queue);

            for (var i = 0; i < 10; i++)
            {
                configureRabbit.Sender(queue, JsonSerializer.Serialize(new { nome = "Richard", id = Guid.NewGuid()}));

            }
            return Ok();
        }
    }
}