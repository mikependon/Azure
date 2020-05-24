using System;

namespace TicksReceiver.Models
{
    public class Tick
    {
        public int Id { get; set; }
        public string Measurement { get; set; }
        public int Value { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}
