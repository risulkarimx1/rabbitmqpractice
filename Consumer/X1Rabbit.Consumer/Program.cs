using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace X1Rabbit.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var queName = "demo-q";
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (o, eventArgs) => OnMessageReceieved(o, eventArgs);
            channel.BasicConsume(queName, true, consumer);
            Console.ReadLine();
        }

        private static void OnMessageReceieved(object? sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"message found : {message}");
        }
    }
}
