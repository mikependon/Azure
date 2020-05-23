using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TicksPublisher.Publishers;
using RepoDb;
using TicksPublisher.Managers;

namespace TicksPublisher
{
    public static class PublishTick
    {
        #region Private

        private static TickManager m_manager = new TickManager();

        #endregion

        static PublishTick()
        {
            Initialize();
        }

        #region Methods

        private static void Initialize()
        {
            SqlServerBootstrap.Initialize();
        }

        #endregion

        [FunctionName("publishtick")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function named '{typeof(PublishTick).Name}' processed a request.");

            try
            {
                var tick = await m_manager.PublishRandomAsync();
                var context = new
                {
                    tick.Id,
                    tick.MeasurementId,
                    tick.Value,
                    tick.CreatedDateUtc
                };
                return new OkObjectResult($"Published: {context}");
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
