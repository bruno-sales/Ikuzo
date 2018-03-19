using System;

namespace Ikuzo.Domain.Entities
{
    public class Gps
    {
        public string BusExternalId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Direction { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
