using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using TicksReceiver.Interfaces;

namespace TicksReceiver.Publishers
{
    public abstract class BaseServiceBusPublisher<T> : IPublisher<T> where T : class
    {
        public BaseServiceBusPublisher(string connectionString,
            string entityPath)
        {
            ConnectionString = connectionString;
            EntityPath = entityPath;
        }

        #region Properties

        public string ConnectionString { get; }

        public string EntityPath { get; }

        #endregion

        #region Methods

        public void Publish(T message)
        {
            var queueClient = new QueueClient(ConnectionString, EntityPath);
            var serialized = JsonConvert.SerializeObject(message);
            var encoded = Encoding.UTF8.GetBytes(serialized);
            queueClient.SendAsync(new Message(encoded));
            queueClient.CloseAsync();
        }

        public async Task PublishAsync(T message)
        {
            var queueClient = new QueueClient(ConnectionString, EntityPath);
            var serialized = JsonConvert.SerializeObject(message);
            var encoded = Encoding.UTF8.GetBytes(serialized);
            await queueClient.SendAsync(new Message(encoded));
            await queueClient.CloseAsync();
        }

        #endregion
    }
}
