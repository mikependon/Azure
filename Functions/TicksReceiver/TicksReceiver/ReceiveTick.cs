using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RepoDb;
using TicksReceiver.Managers;

namespace TicksReceiver
{
    public static class ReceiveTick
    {
        #region Private

        private static TickManager m_manager = new TickManager();

        #endregion

        static ReceiveTick()
        {
            Initialize();
        }

        #region Methods

        private static void Initialize()
        {
            SqlServerBootstrap.Initialize();
        }

        #endregion

        [FunctionName("receivetick")]
        public static void Run(
            [ServiceBusTrigger("ticksqueue", Connection = "TicksServiceBus")]
            string myQueueItem,
            Int32 deliveryCount,
            DateTime enqueuedTimeUtc,
            string messageId,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function named '{typeof(ReceiveTick).Name}' processed a request.");

            try
            {
                //var tick = await m_manager.PublishRandomAsync();
                //var context = new
                //{
                //    tick.Id,
                //    tick.MeasurementId,
                //    tick.Value,
                //    tick.CreatedDateUtc
                //};
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
                log.LogInformation($"EnqueuedTimeUtc={enqueuedTimeUtc}");
                log.LogInformation($"DeliveryCount={deliveryCount}");
                log.LogInformation($"MessageId={messageId}");
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex.Message}");
            }
        }
    }
}
