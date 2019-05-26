using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.DataRio
{
    public interface IDataRioRepository
    {
        IEnumerable<Gps> GetGpsInformation();
        IEnumerable<Itinerary> GetItineraryInformation(string lineId);
    }
}
