using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Producer.Settings;
using RabbitMQ.Client;

namespace Producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var connection = await GetRabbitConnection(configuration);
            var channel = await connection.CreateChannelAsync();
            
            var producer = new Producers.Producer("direct", "exchange.direct", "cars.1", channel);
            //var producer = new Producers.Producer("fanout", "exchange.fanout", "cars.1", channel);
            //var producer = new Producers.Producer("topic", "exchange.topic", "cars.1", channel);
            
            await producer.Produce($"{new Random().Next(1,100)}");
            /*
            while (true)
            {
                await producer.Produce($"{new Random().Next(1,100)}");
                Thread.Sleep(TimeSpan.FromSeconds(1)); // Имитация долгой обработки
            }
*/
            await channel.CloseAsync();
            await connection.CloseAsync();
        }
        
        private static async Task<IConnection> GetRabbitConnection(IConfiguration configuration)
        {
            var rmqSettings = configuration.Get<ApplicationSettings>().RmqSettings;
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = rmqSettings.Host,
                VirtualHost = rmqSettings.VHost,
                UserName = rmqSettings.Login,
                Password = rmqSettings.Password
            };

            return await factory.CreateConnectionAsync();
        }
    }
}
