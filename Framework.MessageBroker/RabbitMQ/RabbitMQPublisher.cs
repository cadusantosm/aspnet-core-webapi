using Framework.Core.Serializer;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MessageBroker.RabbitMQ
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly IConnection _connection;
        private readonly JsonSerializerCommon _serializer;

        public RabbitMQPublisher(RabbitMQConnectionWrapper connection, JsonSerializerCommon serializer)
        {
            _connection = connection.Connection;
            _serializer = serializer;

        }

        public void Publish<T>(T model) where T : BaseMessage
        {
            ProxyPublish(model);
        }

        public async Task PublishAsync<T>(T model) where T : BaseMessage
        {
            await Task.Run(() => { Publish(model); });
        }

        public void Publish<T>(T model, string queueName) where T : BaseMessage
        {
            ProxyPublish(model, queueName);
        }

        public async Task PublishAsync<T>(T model, string queueName) where T : BaseMessage
        {
            await Task.Run(() => { ProxyPublish(model, queueName); });
        }


        private void ProxyPublish<T>(T model, string queueName = "") where T : BaseMessage
        {
            var json = _serializer.Serialize(model);
            var encoded = Encoding.UTF8.GetBytes(json);

            using (var channel = _connection.CreateModel())
            {
                var options = RabbitMQExchangeOptions.Build<T>();

                if (!string.IsNullOrWhiteSpace(queueName))
                    options.QueueName = queueName;

                model.MessageId = Guid.NewGuid();

                BasicPublish(channel, options, encoded);
            }
        }

        private void BasicPublish(IModel channel, RabbitMQExchangeOptions options, byte[] body)
        {
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.CreateModels(options);

            var routingKey = options.ExchangeType == "default" ? options.QueueName : options.RoutingKey;

            channel.BasicPublish(
                                exchange: options.ExchangeName,
                                routingKey: routingKey,
                                basicProperties: properties,
                                body: body);
        }

    }
}