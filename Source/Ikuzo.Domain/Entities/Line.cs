﻿using System;
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
            Tags = new Collection<Tag>();
        }

        public Line(string lineId, string description)
        {
            LineId = lineId;
            Description = description;
            LastUpdateDate = DateTime.UtcNow;
            Modals = new Collection<Modal>();
            Itineraries = new Collection<Itinerary>();
            Tags = new Collection<Tag>();
        }

        public string LineId { get; set; } 
        public string Description { get; set; }
        public virtual Collection<Modal> Modals { get; set; }
        public virtual Collection<Tag> Tags { get; set; }
        public virtual Collection<Itinerary> Itineraries { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
