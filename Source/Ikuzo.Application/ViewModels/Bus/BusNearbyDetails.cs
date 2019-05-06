using System; 

namespace Ikuzo.Application.ViewModels.Bus
{
    public class BusNearbyDetails
    {
        public string Bus { get; set; }
        public string Line { get; set; }
        public float Distance { get; set; }
       // public int MinutesToArrive { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public BusGps Gps { get; set; }
    }
}
