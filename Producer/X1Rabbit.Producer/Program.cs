using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace X1Rabbit.Producer
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

            int c = 0;

            while (true)
            {
                var key =  Console.ReadKey();
                if(key.Key == ConsoleKey.Escape) break;

                var message = new { Name = "Producer", Message = $"Hello {c++}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("", queName, null, body);
            }

            

        }
    }
}
