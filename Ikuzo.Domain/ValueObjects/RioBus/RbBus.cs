using System;

namespace Ikuzo.Domain.ValueObjects.RioBus
{
    public class RbBus
    {
        public string Line { get; set; }
        public string Order { get; set; }
        public int Direction { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
