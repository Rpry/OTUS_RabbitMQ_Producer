using Microsoft.Extensions.Configuration;
using Producer.Producers;
using Producer.Settings;
using RabbitMQ.Client;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            
            //DirectTestProducer.Produce(GetRabbitConnection());
            //FanoutTestProducer.Produce(GetRabbitConnection());
            //TopicTestProducer.Produce(GetRabbitConnection());
            InstantDirectTestProducer.Produce(GetRabbitConnection(configuration));
        }

        static private IConnection GetRabbitConnection(IConfiguration configuration)
        {
            var rmqSettings = configuration.Get<ApplicationSettings>().RmqSettings;
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = rmqSettings.Host,
                VirtualHost = rmqSettings.VHost,
                UserName = rmqSettings.Login,
                Password = rmqSettings.Password,
            };
            IConnection conn = factory.CreateConnection();
            return conn;
        }
    }
}
