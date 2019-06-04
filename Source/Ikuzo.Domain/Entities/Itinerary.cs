using System;
using Newtonsoft.Json;

namespace Ikuzo.Domain.Entities
{
    public class Itinerary
    {
        public Itinerary()
        {
            ItineraryGuid = Guid.NewGuid();
            LastUpdateDate = DateTime.UtcNow;
        } 

        public Guid ItineraryGuid { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Sequence { get; set; }
        public bool Returning { get; set; }
        public decimal? DistanceToNext { get; set; }
        public DateTime LastUpdateDate { get; set; }

        [JsonProperty("line")]
        public string LineId { get; set; }
        public virtual Line Line { get; set; }
    }
}
