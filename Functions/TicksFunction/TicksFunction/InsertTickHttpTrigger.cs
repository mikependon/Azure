using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RepoDb;
using TicksFunction.Models;
using TicksFunction.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace TicksFunction
{
    public static class InsertTickHttpTrigger
    {
        #region Methods

        private static void Bootstrap()
        {
            SqlServerBootstrap.Initialize();
        }

        private static T GetAny<T>(IEnumerable<T> elements)
        {
            var index = Randomizer.Next(0, elements.Count() - 1);
            return elements.ElementAt(index);
        }

        private static Tick GenerateTicks(Measurement measurement)
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

        static InsertTickHttpTrigger()
        {
            Bootstrap();
        }

        [FunctionName("InsertTicksHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function named '{typeof(InsertTickHttpTrigger).Name}' processed a request.");

            using (var repository = new DatabaseRepository())
            {
                try
                {
                    var measurements = repository.QueryAll<Measurement>(cacheKey: "Measurements");
                    var measurement = GetAny(measurements);
                    var ticks = GenerateTicks(measurement);
                    var context = new
                    {
                        measurement.Name,
                        ticks.Value,
                        ticks.CreatedDateUtc
                    };

                    log.LogInformation($"Ticks generated: '{context}'.");

                    var id = await repository.InsertAsync(ticks);

                    var message = $"Tick record '{context}' has been saved with ID = '{id}'.";
                    log.LogInformation(message);

                    return new OkObjectResult(message);
                }
                catch (Exception ex)
                {
                    log.LogError($"Error: {ex.Message}");
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }
    }
}
