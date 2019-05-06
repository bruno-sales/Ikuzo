using System;
using System.Collections.ObjectModel;

namespace Ikuzo.Domain.Entities
{
    public class Line
    {
        public Line()
        {
            LastUpdateDate = DateTime.UtcNow;
            Buses = new Collection<Bus>();
            Itineraries = new Collection<Itinerary>();
            Tags = new Collection<Tag>();
        }

        public Line(string lineId, string description)
        {
            LineId = lineId;
            Description = description;
            LastUpdateDate = DateTime.UtcNow;
            Buses = new Collection<Bus>();
            Itineraries = new Collection<Itinerary>();
            Tags = new Collection<Tag>();
        }

        public string LineId { get; set; } 
        public string Description { get; set; }
        public virtual Collection<Bus> Buses { get; set; }
        public virtual Collection<Tag> Tags { get; set; }
        public virtual Collection<Itinerary> Itineraries { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
