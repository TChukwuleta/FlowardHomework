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
        private readonly IRabbitMQConfiguration _rabbitMQConfiguration;
        private readonly IConfiguration _config;
        private readonly IBrokerProvider _brokerProvider;
        public BrokerService(IBrokerProvider brokerProvider, IRabbitMQConfiguration rabbitMQConfiguration, IConfiguration config)
        {
            _brokerProvider = brokerProvider;
            _rabbitMQConfiguration = rabbitMQConfiguration;
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

        public void SendMessage<T>(List<T> messages, ProductType productType)
        {
            if (!messages.Any()) return;
            var rabbitConfig = _rabbitMQConfiguration.GetRabbitMQConfigData();
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messages));
            _brokerProvider.RabbitMQChannel.BasicPublish(rabbitConfig.Exchange, $"{productType}", true, null, body);
        }

        public T ReceiveMessage<T>()
        {
            var message = "";
            var rabbitConfig = _rabbitMQConfiguration.GetRabbitMQConfigData();
            var consumer = new EventingBasicConsumer(_brokerProvider.RabbitMQChannel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                message = Encoding.UTF8.GetString(body);
                if (string.IsNullOrEmpty(message))
                {
                    _brokerProvider.RabbitMQChannel.BasicAck(ea.DeliveryTag, false);
                }
            };
            Dictionary<string, object> dict = new Dictionary<string, object>();
            _brokerProvider.RabbitMQChannel.BasicConsume(rabbitConfig.Queue, false, message, false, false, dict, consumer);
            return JsonConvert.DeserializeObject<T>(message);
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
                    
                }
            };
            channel.BasicConsume(queue: "user",
                autoAck: true,
                consumer: consumer);
        }
    }
}
