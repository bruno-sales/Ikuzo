using System;

namespace Ikuzo.Domain.Entities
{
    public class Bus
    {
        public int BusId { get; set; }
        public string ExternalId { get; set; }
        public string Line { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
