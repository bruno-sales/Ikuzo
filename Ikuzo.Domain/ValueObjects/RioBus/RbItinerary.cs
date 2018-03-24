using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ikuzo.Domain.ValueObjects.RioBus
{
    public class RbItinerary
    {
        public RbItinerary()
        {
            Spots = new Collection<RbSpot>();
        }

        public string Line { get; set; }
        public virtual IEnumerable<RbSpot> Spots { get; set; }
    }

    public class RbSpot
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Returning { get; set; }
    }
}
