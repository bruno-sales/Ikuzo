using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects.RioBus;

namespace Ikuzo.Domain.Interfaces.CrossCuttings
{
    public interface IRioBusRepository
    {
        IEnumerable<RbLine> GetAllLines();
        IEnumerable<RbBus> GetBusesInfoFromLine(string lineId);
        IEnumerable<Gps> GetGpsInfoFromLines(List<string> lineIds); 
        RbItinerary GetItineraryInfoFromLine(string lineId);
    }
}
