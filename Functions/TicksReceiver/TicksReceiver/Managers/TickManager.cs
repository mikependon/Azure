using System;
using System.Threading.Tasks;
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

        #region Methods

        public Tick Publish(DTOs.Tick tick)
        {
            try
            {
                return default;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Tick> PublishAsync()
        {
            try
            {
                return default;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
