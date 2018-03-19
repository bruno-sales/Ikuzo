using System;

namespace Ikuzo.Domain.Entities
{
    public class Bus
    {
        public Bus()
        {
            CreateDate = DateTime.Now;
        }

        public Bus(int lineId, string externalId)
        {
            LineId = lineId;
            ExternalId = externalId;
            CreateDate = DateTime.Now;
        }

        public int BusId { get; set; }
        public string ExternalId { get; set; }
        public int LineId { get; set; }
        public virtual Line Line { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
