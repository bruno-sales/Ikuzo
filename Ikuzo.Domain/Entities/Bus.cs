using System;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Ikuzo.Domain.Entities
{
    public class Bus
    {
        public Bus()
        {
            BusGuid = Guid.NewGuid();
            LastUpdateDate = DateTime.Now;
        }

        public Bus(string busId, string lineId)
        {
            BusGuid = Guid.NewGuid();
            BusId = busId;
            LineId = lineId;
            LastUpdateDate = DateTime.Now;
        }

        public Guid BusGuid { get; set; }
        public string BusId { get; set; }
        public string LineId { get; set; }
        public virtual Line Line { get; set; }
        [NotMapped]
        public Gps Gps { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
