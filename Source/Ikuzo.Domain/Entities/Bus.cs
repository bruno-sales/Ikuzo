using System;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Ikuzo.Domain.Entities
{
    public class Bus
    {
        public Bus()
        {
            LastUpdateDate = DateTime.Now;
        }

        public Bus(string busId, string lineId)
        {
            BusId = busId;
            LineId = lineId;
            LastUpdateDate = DateTime.Now;
        }

        public string BusId { get; set; }
        public string LineId { get; set; }
        public virtual Line Line { get; set; }
        [NotMapped]
        public Gps Gps { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
