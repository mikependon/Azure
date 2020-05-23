using System;

namespace TicksPublisher.Models
{
    public class Tick
    {
        public int Id { get; set; }
        public int MeasurementId { get; set; }
        public int Value { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}
