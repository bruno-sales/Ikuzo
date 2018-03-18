using System;

namespace Ikuzo.Domain.Entities
{
    public class Bus
    {
        public int BusId { get; set; }
        public string ExternalId { get; set; }
        public int LineId { get; set; }
        public virtual Line Line { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
