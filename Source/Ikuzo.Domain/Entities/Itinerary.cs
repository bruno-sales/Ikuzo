using System;
using Newtonsoft.Json;

namespace Ikuzo.Domain.Entities
{
    public class Itinerary
    {
        public Itinerary()
        {
            ItineraryGuid = Guid.NewGuid();
            LastUpdateDate = DateTime.Now;
        }
        public Itinerary(string lineId, decimal lat, decimal longe, bool returning)
        {
            LineId = lineId;
            Latitude = lat;
            Longitude = longe;
            Returning = returning;
            ItineraryGuid = Guid.NewGuid();
            LastUpdateDate = DateTime.Now;
        }

        public Guid ItineraryGuid { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Returning { get; set; }
        public DateTime LastUpdateDate { get; set; }

        [JsonProperty("line")]
        public string LineId { get; set; }
        public virtual Line Line { get; set; }
    }
}
