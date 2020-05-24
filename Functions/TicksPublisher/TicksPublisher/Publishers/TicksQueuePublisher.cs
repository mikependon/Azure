using TicksPublisher.DTOs;

namespace TicksPublisher.Publishers
{
    public class TicksQueuePublisher : BaseServiceBusQueuePublisher<Tick>
    {
        public TicksQueuePublisher() :
            base("ticksqueue", "Endpoint=sb://ticksservicebusnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=UzPz5w6wVOzG1AIe31jafmzbOfhCrbdQEFV3t7JHlsw=")
        { }
    }
}
