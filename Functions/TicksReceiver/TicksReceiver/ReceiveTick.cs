using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepoDb;
using TicksReceiver.DTOs;
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
        public static async void Run(
            [ServiceBusTrigger("ticksqueue", Connection = "TicksServiceBus")]
            string message,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function named '{typeof(ReceiveTick).Name}' processed a request. Message: {message}");

            try
            {
                var tick = JsonConvert.DeserializeObject<TickDTO>(message);
                var result = await m_manager.SaveAndPublishAsync(tick);
                log.LogInformation($"Tick has been saved and published '{JsonConvert.SerializeObject(result)}'.");
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex.Message}");
            }
        }
    }
}
