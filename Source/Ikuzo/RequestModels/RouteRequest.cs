using System.Collections.Generic;

namespace Ikuzo.RequestModels
{
    public class RouteRequest
    {
        public RouteRequest()
        {
            Tags = new List<string>();
        }

        public decimal LatOri { get; set; }
        public decimal LonOri { get; set; }
        public decimal LatDest { get; set; }
        public decimal LonDest { get; set; }
        public List<string> Tags { get; set; }
    }
}