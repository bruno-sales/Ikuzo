using System.Collections.Generic;

namespace Ikuzo.RequestModels
{
    public class LocalLinesRequest
    {
        public LocalLinesRequest()
        {
            Tags = new List<string>();
        }

        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public decimal? Distance { get; set; }
        public List<string> Tags { get; set; }
    }
}