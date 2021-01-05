namespace Ikuzo.Domain.Entities
{
    public class LineTag
    {
        public int LineTagId { get; set; }
        public string LineId { get; set; }
        public int TagId { get; set; }
        public virtual Line Line { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
