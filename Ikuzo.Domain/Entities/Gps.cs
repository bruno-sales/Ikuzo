using System;
using Newtonsoft.Json;

namespace Ikuzo.Domain.Entities
{
    public class Gps
    {
        public Gps()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [JsonProperty("order")]
        public string BusExternalId { get; set; }
        [JsonProperty("line")]
        public string LineExternalId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Direction { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
