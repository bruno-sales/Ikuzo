namespace Ikuzo.RequestModels
{
    public class LocalLinesRequest
    {
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public decimal? Precision { get; set; }
    }
}