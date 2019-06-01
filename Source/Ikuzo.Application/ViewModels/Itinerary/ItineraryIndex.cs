using System; 

namespace Ikuzo.Application.ViewModels.Itinerary
{
    public class ItineraryIndex
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Sequence { get; set; }
        public bool Returning { get; set; } 
    }
}
