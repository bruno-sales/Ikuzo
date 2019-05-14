using System; 

namespace Ikuzo.Domain.Histories
{
    public class GpsHistory
    { 
        public Int64 GpsHistoryId { get; set; }
        public string BusId { get; set; } 
        public string LineId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Direction { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
