using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using TicksPublisher.Interfaces;

namespace TicksPublisher.Publishers
{
    public abstract class BaseServiceBusQueuePublisher<T> : IPublisher<T> where T : class
    {
        public BaseServiceBusQueuePublisher(string queueName,
            string connectionString)
        {
            QueueName = queueName;
            ConnectionString = connectionString;
        }

        #region Properties

        public string QueueName { get; }

        public string ConnectionString { get; }

        #endregion

        #region Methods

        public void Publish(T message)
        {
            var queueClient = new QueueClient(ConnectionString, QueueName);
            var serialized = JsonConvert.SerializeObject(message);
            var encoded = Encoding.UTF8.GetBytes(serialized);
            queueClient.SendAsync(new Message(encoded));
            queueClient.CloseAsync();
        }

        public async Task PublishAsync(T message)
        {
            var queueClient = new QueueClient(ConnectionString, QueueName);
            var serialized = JsonConvert.SerializeObject(message);
            var encoded = Encoding.UTF8.GetBytes(serialized);
            await queueClient.SendAsync(new Message(encoded));
            await queueClient.CloseAsync();
        }

        #endregion
    }
}
