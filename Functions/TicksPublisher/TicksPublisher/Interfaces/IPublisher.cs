namespace TicksPublisher.Interfaces
{
    public interface IPublisher<T>
    {
        void Publish(T message);
    }
}
