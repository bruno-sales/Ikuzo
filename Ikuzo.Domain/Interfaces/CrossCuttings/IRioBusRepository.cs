using System.Collections.Generic;
using Ikuzo.Domain.Entities;
using Ikuzo.Domain.ValueObjects.RioBus;

namespace Ikuzo.Domain.Interfaces.CrossCuttings
{
    public interface IRioBusRepository
    {
        IEnumerable<RbLine> GetAllLines();
        IEnumerable<RbBus> GetBusesInfoFromLine(string lineId);
        IEnumerable<Gps> GetGpsInfoFromLine(string lineId);
        IEnumerable<Gps> GetGpsInfoFromBus(string busId);
        RbItinerary GetItineraryInfoFromLine(string lineId);
    }
}
