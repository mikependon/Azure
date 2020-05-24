using System.Threading.Tasks;

namespace TicksReceiver.Interfaces
{
    public interface IPublisher<T>
    {
        void Publish(T message);
        Task PublishAsync(T message);
    }
}
