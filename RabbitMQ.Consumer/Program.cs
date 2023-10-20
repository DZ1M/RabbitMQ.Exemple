// See https://aka.ms/new-console-template for more information
using RabbitMQ.Message;

Console.WriteLine("Iniciando consumo");

var configureRabbit = new RabbitConnection();

var queue = "apenas-teste";

configureRabbit.Configure();
configureRabbit.DefineQueue(queue);

while (true)
{
    configureRabbit.Consumer(queue);
    Thread.Sleep(1000);
}