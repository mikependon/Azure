using System.Threading.Tasks;

namespace TicksPublisher.Interfaces
{
    public interface IPublisher<T>
    {
        void Publish(T message);
        Task PublishAsync(T message);
    }
}
