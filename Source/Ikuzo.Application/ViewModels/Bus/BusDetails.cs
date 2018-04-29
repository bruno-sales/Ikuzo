using System;

namespace Ikuzo.Application.ViewModels.Bus
{
    public class BusDetails
    {
        public string Bus { get; set; }
        public string Line { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public BusGps Gps { get; set; }
    }
}
