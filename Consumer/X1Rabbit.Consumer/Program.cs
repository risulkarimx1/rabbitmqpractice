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
            
            //QueConsumer.Consume(channel, queName);
            DirectExchangeConsumer.Consume(channel);
        }

       
    }
}
