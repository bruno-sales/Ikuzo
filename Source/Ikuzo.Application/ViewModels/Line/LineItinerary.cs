using System;
using System.Collections.Generic;
using Ikuzo.Application.ViewModels.Itinerary; 

namespace Ikuzo.Application.ViewModels.Line
{
    public class LineItinerary
    {
        public LineItinerary()
        {
            Itineraries = new List<ItineraryIndex>();
        }

        public string Line { get; set; }
        public string Name { get; set; } 

        public List<ItineraryIndex> Itineraries { get; set; }
    }

}
