using System;

namespace TicksReceiver.Models
{
    public class Tick
    {
        public int Id { get; set; }
        public int MeasurementId { get; set; }
        public decimal Value { get; set; }
        public string Receiver { get; set; }
        public string Version { get; set; }
        public DateTime PublishedDateUtc { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}
