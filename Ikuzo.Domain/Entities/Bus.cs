using System;

namespace Ikuzo.Domain.Entities
{
    public class Bus
    {
        public Bus()
        {
            CreateDate = DateTime.Now;
            BusId = Guid.NewGuid();
        }

        public Bus(Guid lineId, string externalId)
        {
            BusId = Guid.NewGuid();
            LineId = lineId;
            ExternalId = externalId;
            CreateDate = DateTime.Now;
        }

        public Guid BusId { get; set; }
        public string ExternalId { get; set; }
        public Guid LineId { get; set; }
        public virtual Line Line { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
