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

            QueueProducer.Publish(channel, queName);
        }
    }
}
