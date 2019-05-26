using System;

namespace Ikuzo.Application.ViewModels.Modal
{
    public class ModalNearbyDetails
    {
        public string Modal { get; set; }
        public string Line { get; set; }
        public float Distance { get; set; }
       // public int MinutesToArrive { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public ModalGps Gps { get; set; }
    }
}
