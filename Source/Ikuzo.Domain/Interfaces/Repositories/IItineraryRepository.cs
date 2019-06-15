using System.Collections.Generic;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Domain.Interfaces.Repositories
{
    public interface IItineraryRepository : IBaseRepository<Itinerary>
    {
        IEnumerable<Line> GetLocalLines(decimal latitude, decimal longitude, decimal distance);
        void RemoveFromLine(string lineId);
        void RemoveAll();
        void ItineraryBulkInsert(IEnumerable<Itinerary> itineraries);
        IEnumerable<Itinerary> GetLineItineraries(string lineId);
        IEnumerable<Line> GetLocalToDestinyLines(decimal latitude1, decimal longitude1, decimal latitude2,
            decimal longitude2, decimal distance);
    }
}
