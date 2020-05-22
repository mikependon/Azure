using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using RepoDb;
using TicksFunction.Models;

namespace TicksFunction
{
    public static class InsertTicksHttpTrigger
    {
        #region Properties

        static string ConnectionString => "Server=tcp:mipensqlserver.database.windows.net,1433;Initial Catalog=TestDB;Persist Security Info=False;User ID=mipen;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        #endregion

        #region Methods

        private static void Bootstrap()
        {
            SqlServerBootstrap.Initialize();
        }

        private static async Task<long> Save(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await connection.InsertAsync<Ticks, long>(new Ticks
                {
                    CreatedDateUtc = DateTime.UtcNow,
                    Name = name
                });
            }
        }

        #endregion

        static InsertTicksHttpTrigger()
        {
            Bootstrap();
        }

        [FunctionName("InsertTicksHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // Log the message
            log.LogInformation($"C# HTTP trigger function named '{typeof(InsertTicksHttpTrigger).Name}' processed a request.");

            // Get the request
            var name = (string)req.Query["name"];
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = (dynamic)JsonConvert.DeserializeObject(requestBody);

            // Ensure the name
            name = name ?? data?.name ?? $"Name-{Guid.NewGuid().ToString()}";

            // Log the message
            log.LogInformation($"The name to be saved is '{name}'.");
            log.LogInformation("Saving to database, please wait...");

            // Save to DB
            try
            {
                var id = await Save(name);

                // Log the message
                var message = $"New tick record has been saved with id = {id}.";
                log.LogInformation(message);
                return (ActionResult)new OkObjectResult(message);
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex.Message}");
                return (ActionResult)new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
