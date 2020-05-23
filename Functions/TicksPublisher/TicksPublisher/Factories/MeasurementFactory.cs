using System.Collections.Generic;
using TicksPublisher.Models;

namespace TicksPublisher.Factories
{
    public static class MeasurementFactory
    {
        #region Privates

        private static IList<Measurement> m_measurements;

        #endregion

        static MeasurementFactory()
        {
            Initialize();
        }

        #region Methods

        private static void Initialize()
        {
            m_measurements = new List<Measurement>();
            m_measurements.Add(new Measurement { Name = "Windspeed", MaxValue = 500 });
            m_measurements.Add(new Measurement { Name = "ActivePower", MaxValue = 3600 });
            m_measurements.Add(new Measurement { Name = "YawAngle", MaxValue = 360 });
            m_measurements.Add(new Measurement { Name = "WindDirection", MaxValue = 360 });
            m_measurements.Add(new Measurement { Name = "RotorTemp", MaxValue = 1000 });
            m_measurements.Add(new Measurement { Name = "LubricantLevel", MaxValue = 10 });
        }

        public static IEnumerable<Measurement> GetMeasurements()
        {
            return m_measurements;
        }

        #endregion
    }
}
