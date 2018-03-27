namespace Ikuzo.RequestModels
{
    public class NerbyBusesRequest
    {
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public decimal? Var { get; set; }
        public string Line { get; set; }
    }
}