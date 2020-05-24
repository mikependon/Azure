﻿using System;

namespace TicksPublisher.DTOs
{
    public class Tick
    {
        public string Measurement { get; set; }
        public int Value { get; set; }
        public DateTime PublishedDateUtc { get; set; }
    }
}
