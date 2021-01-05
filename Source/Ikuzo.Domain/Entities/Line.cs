using System;
using System.Collections.ObjectModel;

namespace Ikuzo.Domain.Entities
{
    public class Line
    {
        public Line()
        {
            LastUpdateDate = DateTime.UtcNow;
            Modals = new Collection<Modal>();
            Itineraries = new Collection<Itinerary>();
            LineTags = new Collection<LineTag>();
        }

        public Line(string lineId, string description)
        {
            LineId = lineId;
            Description = description;
            LastUpdateDate = DateTime.UtcNow;
            Modals = new Collection<Modal>();
            Itineraries = new Collection<Itinerary>();
            LineTags = new Collection<LineTag>();
        }

        public string LineId { get; set; } 
        public string Description { get; set; }
        public virtual Collection<Modal> Modals { get; set; }
        public virtual Collection<LineTag> LineTags { get; set; }
        public virtual Collection<Itinerary> Itineraries { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
