namespace Ikuzo.RequestModels
{
    public class NerbyModalsRequest
    {
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public decimal? Distance { get; set; }
        public string Line { get; set; }
    }
}