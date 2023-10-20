using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Message.Config;
using RabbitMQ.Message.Interfaces;
using RabbitMQ.Message.Services;

namespace RabbitMQ.Message
{
    public static class DependencyInject
    {
        public static void InjectRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfig>(a => configuration.GetSection(nameof(RabbitMqConfig)).Bind(a));
            services.AddSingleton<IRabbitConnection, RabbitConnection>();

            services.AddSingleton<IRabbitSender, RabbitSender>();
            services.AddSingleton<IRabbitConsumer, RabbitConsumer>();
        }
    }
}
