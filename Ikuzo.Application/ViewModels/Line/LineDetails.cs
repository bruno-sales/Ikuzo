using System;
using System.Collections.Generic; 

namespace Ikuzo.Application.ViewModels.Line
{
    public class LineDetails
    {
        public string Line { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public IEnumerable<LineBus> Buses { get; set; }
        public IEnumerable<LineItinerary> Itineraries { get; set; }
    }
}
