using System;
using System.Threading.Tasks;
using TicksPublisher.Extensions;
using TicksPublisher.Models;
using TicksPublisher.Publishers;
using TicksPublisher.Repositories;

namespace TicksPublisher.Managers
{
    public class TickManager
    {
        #region Privates

        private static Random m_randomizer = new Random();
        private static DatabaseRepository m_repository = new DatabaseRepository();
        private static TickServiceBusPublisher m_publisher = new TickServiceBusPublisher();

        #endregion

        #region Methods

        public Tick PublishRandom()
        {
            try
            {
                var measurements = m_repository.QueryAll<Measurement>(cacheKey: "Measurements");
                var tick = CreateFromMeasurement(measurements.GetAny());
                m_repository.Insert(tick);
                m_publisher.Publish(tick);
                return tick;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Tick> PublishRandomAsync()
        {
            try
            {
                var measurements = await m_repository.QueryAllAsync<Measurement>(cacheKey: "Measurements");
                var tick = CreateFromMeasurement(measurements.GetAny());
                await m_repository.InsertAsync(tick);
                await m_publisher.PublishAsync(tick);
                return tick;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Helpers

        private static Tick CreateFromMeasurement(Measurement measurement)
        {
            return new Tick
            {
                MeasurementId = measurement.Id,
                Value = m_randomizer.Next(0, measurement.MaxValue),
                CreatedDateUtc = DateTime.UtcNow
            };
        }

        #endregion
    }
}
