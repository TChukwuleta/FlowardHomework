using Floward.Domain.Enums;
using Floward.Domain.Interfaces.MessageBroker;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Broker
{
    public class BrokerService : IBrokerService
    {
        private readonly IConfiguration _config;
        public BrokerService(IConfiguration config)
        {
            _config = config;
        }

        public void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: _config["RabbitMQ:Exchange"],
                routingKey: integrationEvent,
                basicProperties: null,
                body: body);
        }


        public void Received()
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                var data = JObject.Parse(message);
                var type = ea.RoutingKey;
                if(type == "user.create")
                {
                    // Add and save to DB   
                }
            };
            channel.BasicConsume(queue: "user.orderservice",
                autoAck: true,
                consumer: consumer);
        }
    }
}
