using RabbitMq.Consumer;
using RabbitMQ.Message;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.InjectRabbitMq(hostContext.Configuration);
    })
    .Build();

host.Run();
