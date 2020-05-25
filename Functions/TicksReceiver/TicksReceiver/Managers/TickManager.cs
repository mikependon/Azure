using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TicksReceiver.DTOs;
using TicksReceiver.Models;
using TicksReceiver.Publishers;
using TicksReceiver.Repositories;

namespace TicksReceiver.Managers
{
    public class TickManager
    {
        #region Privates

        private static Random m_randomizer = new Random();
        private static ProductionRepository m_repository = new ProductionRepository();
        private static TickServiceBusPublisher m_publisher = new TickServiceBusPublisher();

        #endregion

        #region Helpers

        public Measurement GetAndEnsureMeasurement(string name)
        {
            var measurement = m_repository
                .QueryAll<Measurement>(cacheKey: "Measurements")
                .FirstOrDefault(e => string.Equals(name, e.Name, StringComparison.OrdinalIgnoreCase));
            if (measurement == null)
            {
                throw new InvalidOperationException($"Measurement '{name}' is not found.");
            }
            return measurement;
        }

        public Tick ToTick(TickDTO tick)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            return new Tick
            {
                MeasurementId = GetAndEnsureMeasurement(tick.Measurement).Id,
                PublishedDateUtc = tick.PublishedDateUtc,
                Value = tick.Value,
                Receiver = assemblyName.Name,
                Version = assemblyName.Version?.ToString(),
                CreatedDateUtc = DateTime.UtcNow
            };
        }

        #endregion

        #region Methods

        public Tick SaveAndPublish(TickDTO tick)
        {
            try
            {
                var converted = ToTick(tick);
                m_repository.Insert(converted);
                return converted;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Tick> SaveAndPublishAsync(TickDTO tick)
        {
            try
            {
                var converted = ToTick(tick);
                await m_repository.InsertAsync(converted);
                return converted;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
