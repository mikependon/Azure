using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TicksPublisher.Models;
using System.Collections.Generic;
using System.Linq;
using TicksPublisher.Factories;
using TicksPublisher.Publishers;

namespace TicksPublisher
{
    public static class PublishTick
    {
        #region Methods

        private static T GetAny<T>(IEnumerable<T> elements)
        {
            var index = Randomizer.Next(0, elements.Count() - 1);
            return elements.ElementAt(index);
        }

        private static Tick CreateTickFromMeasurement(Measurement measurement)
        {
            return new Tick
            {
                MeasurementId = measurement.Id,
                Value = Randomizer.Next(0, measurement.MaxValue),
                CreatedDateUtc = DateTime.UtcNow
            };
        }

        #endregion

        #region Properties

        public static Random Randomizer => new Random();

        #endregion

        [FunctionName("publishtick")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function named '{typeof(PublishTick).Name}' processed a request.");

            var measurements = MeasurementFactory.GetMeasurements();
            var measurement = GetAny(measurements);
            var tick = CreateTickFromMeasurement(measurement);
            var context = new
            {
                measurement.Name,
                tick.Value,
                tick.CreatedDateUtc
            };
            log.LogInformation($"Created: '{context}'");


            try
            {
                var publisher = new TickServiceBusPublisher();
                publisher.Publish(tick);
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
