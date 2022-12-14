using Producer.Producers;
using RabbitMQ.Client;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectTestProducer.Produce(GetRabbitConnection());
            // FanoutTestProducer.Produce(GetRabbitConnection());
            // TopicTestProducer.Produce(GetRabbitConnection());
            // InstantDirectTestProducer.Produce(GetRabbitConnection());
        }

        static private IConnection GetRabbitConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "xvvcjzoi",
                Password = "3zzqgto8t6iqz6EMWhrx3fj8ubnToHJ6",
                VirtualHost = "xvvcjzoi",
                HostName = "cow.rmq2.cloudamqp.com"
            };
            IConnection conn = factory.CreateConnection();
            return conn;
        }
    }
}
