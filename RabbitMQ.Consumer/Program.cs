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
    // Adicione uma pausa para evitar que o loop consuma muitos recursos
    System.Threading.Thread.Sleep(1000); // Espere 1 segundo antes de verificar novamente
}