﻿using System;
using System.Threading.Tasks;
using TicksPublisher.DTOs;
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
        private static DefinitionRepository m_repository = new DefinitionRepository();
        private static TicksQueuePublisher m_publisher = new TicksQueuePublisher();

        #endregion

        #region Methods

        public TickDTO PublishRandom()
        {
            try
            {
                var measurements = m_repository.QueryAll<Measurement>(cacheKey: "Measurements");
                var tick = CreateFromMeasurement(measurements.GetAny());
                m_repository.Insert(tick);
                return tick;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TickDTO> PublishRandomAsync()
        {
            try
            {
                var measurements = await m_repository.QueryAllAsync<Measurement>(cacheKey: "Measurements");
                var tick = CreateFromMeasurement(measurements.GetAny());
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

        private static TickDTO CreateFromMeasurement(Measurement measurement)
        {
            return new TickDTO
            {
                Measurement = measurement.Name,
                Value = m_randomizer.Next(0, measurement.MaxValue),
                PublishedDateUtc = DateTime.UtcNow
            };
        }

        #endregion
    }
}
