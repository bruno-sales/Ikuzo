using System;
using System.Collections.ObjectModel;

namespace Ikuzo.Domain.Entities
{
    public class Line
    {
        public Line()
        {
            Buses = new Collection<Bus>();
        }

        public Line(string externalId, string description)
        {
            ExternalId = externalId;
            Description = description;
            CreateDate = DateTime.Now;
            Buses = new Collection<Bus>();
        }

        public int LineId { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }
        public virtual Collection<Bus> Buses { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
