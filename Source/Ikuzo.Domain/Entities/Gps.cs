using System;
using Newtonsoft.Json;

namespace Ikuzo.Domain.Entities
{
    public class Gps
    {
        public Gps()
        {
            GpsGuid = Guid.NewGuid();
            LastUpdateDate = DateTime.Now;
        }

        public Guid GpsGuid { get; set; }

        [JsonProperty("order")]
        public string BusId { get; set; } 
        [JsonProperty("line")]
        public string LineId { get; set; } 
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Direction { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
