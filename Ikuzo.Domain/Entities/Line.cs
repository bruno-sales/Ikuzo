using System;
using System.Collections.ObjectModel;

namespace Ikuzo.Domain.Entities
{
    public class Line
    {
        public Line()
        {
            Buses = new Collection<Bus>(); 
        }

        public Line(string lineId, string description)
        {
            LineId = lineId;
            Description = description;
            LastUpdateDate = DateTime.Now;
            Buses = new Collection<Bus>();
        }

        public string LineId { get; set; } 
        public string Description { get; set; }
        public virtual Collection<Bus> Buses { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
