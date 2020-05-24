using TicksReceiver.Models;

namespace TicksReceiver.Publishers
{
    public class TickServiceBusPublisher : BaseServiceBusPublisher<Tick>
    {
        public TickServiceBusPublisher() :
            base("Endpoint=sb://ticksservicebusnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=UzPz5w6wVOzG1AIe31jafmzbOfhCrbdQEFV3t7JHlsw=", "ticksqueue")
        { }
    }
}
