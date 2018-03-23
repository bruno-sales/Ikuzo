using System;
using Newtonsoft.Json;

namespace Ikuzo.Domain.Entities
{
    public class Gps
    {
        public Gps()
        {
            GpsId = Guid.NewGuid();
        }

        public Guid GpsId { get; set; }

        [JsonProperty("order")]
        public string BusExternalId { get; set; }
        [JsonProperty("line")]
        public string LineExternalId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Direction { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
