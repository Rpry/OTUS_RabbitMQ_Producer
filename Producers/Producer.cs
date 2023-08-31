using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Producer.Producers
{
    public class Producer
    {
        private string _exchangeType;
        private string _exchangeName;
        private string _routingKey;
        private IModel _model;
        public Producer(string exchangeType, string exchangeName, string routingKey, IModel model)
        {
            _exchangeName = exchangeName;
            _exchangeType = exchangeType;
            _routingKey = routingKey;
            _model = model;
            _model.ExchangeDeclare(_exchangeName, _exchangeType);
        }
        
        public void Produce(string messageContent)
        {
            var message = new MessageDto()
            {
                Content = $"{messageContent} (exchange: {_exchangeType})"
            };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            
            _model.BasicPublish(exchange: _exchangeName,
                routingKey: _routingKey,
                basicProperties: null,
                body: body);

            Console.WriteLine($"Message is sent into Default Exchange: {_exchangeName}");
        }
    }
}