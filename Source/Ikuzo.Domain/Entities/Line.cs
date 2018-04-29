using System;
using System.Collections.ObjectModel;

namespace Ikuzo.Domain.Entities
{
    public class Line
    {
        public Line()
        {
            LastUpdateDate = DateTime.Now;
            Buses = new Collection<Bus>();
            Itineraries = new Collection<Itinerary>();
        }

        public Line(string lineId, string description)
        {
            LineId = lineId;
            Description = description;
            LastUpdateDate = DateTime.Now;
            Buses = new Collection<Bus>();
            Itineraries = new Collection<Itinerary>();
        }

        public string LineId { get; set; } 
        public string Description { get; set; }
        public virtual Collection<Bus> Buses { get; set; }
        public virtual Collection<Itinerary> Itineraries { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
